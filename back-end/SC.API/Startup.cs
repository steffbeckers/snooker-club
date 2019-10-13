using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SC.API.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using SC.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SC.API.DAL.Repositories;
using GraphQL;
using SC.API.GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace SC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Connection to the SnookerClub database
            services.AddDbContext<SCContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SCContext")));

            // Repositories
            services.AddScoped<BreakRepository>();
            services.AddScoped<FrameRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<LeagueRepository>();
            services.AddScoped<PlayerRepository>();
            services.AddScoped<SettingRepository>();
            services.AddScoped<TournamentRepository>();
            services.AddScoped<UserRepository>();

            // GraphQL
            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<SCSchema>();
            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true;
            }).AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User);

            // Authentication
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddEntityFrameworkStores<SCContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Sign in
                options.SignIn.RequireConfirmedEmail = false;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // JWT's
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Authentication").GetValue<string>("Secret"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });

            // AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // MVC
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.MaxDepth = 3;
                    // camelCase
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            // Kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // IIS Express
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Update database migrations on startup
            UpdateDatabase(app);

            // GraphQL
            app.UseGraphQL<SCSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            // MVC
            app.UseRouting();

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();
            CreateDefaultRolesAndAdminUser(serviceProvider);

            // MVC
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SCContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private void CreateDefaultRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Roles

            Task<IdentityRole<Guid>> adminRole = roleManager.FindByNameAsync("Admin");
            adminRole.Wait();
            if (adminRole.Result == null)
            {
                IdentityRole<Guid> newAdminRole = new IdentityRole<Guid>()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                var createAdminRole = roleManager.CreateAsync(newAdminRole);
                createAdminRole.Wait();
            }

            // Check if the Admin user exists and create it if not
            // Add to the Admin role

            Task<User> adminUser = userManager.FindByNameAsync(Configuration.GetSection("Admin").GetValue<string>("Username"));
            adminUser.Wait();
            if (adminUser.Result == null)
            {
                User newAdminUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = Configuration.GetSection("Admin").GetValue<string>("Email"),
                    UserName = Configuration.GetSection("Admin").GetValue<string>("Username"),
                    FirstName = Configuration.GetSection("Admin").GetValue<string>("FirstName"),
                    LastName = Configuration.GetSection("Admin").GetValue<string>("LastName"),
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(newAdminUser, Configuration.GetSection("Admin").GetValue<string>("Password"));
                newUser.Wait();
                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(newAdminUser, "Admin");
                    newUserRole.Wait();
                }
            }
        }
    }
}
