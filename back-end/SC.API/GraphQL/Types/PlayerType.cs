using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class PlayerType : ObjectGraphType<Player>
    {
        public PlayerType(UserRepository userRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.FirstName, nullable: true).Description("The first name of the player");
            Field(x => x.LastName, nullable: true).Description("The last name of the player");
            Field(x => x.Score, nullable: true).Description("The score of the player within frame or break");
            Field(x => x.Handicap, nullable: true).Description("The handicap value of the player within league, tournament or frame");
            Field(x => x.Position, nullable: true).Description("The position of the player within group or frame");

            Field(x => x.UserId, type: typeof(IdGraphType), nullable: true);
            Field<UserType>(
                "user",
                resolve: context => {
                    ClaimsPrincipal user = (ClaimsPrincipal)context.UserContext;
                    if (!user.IsInRole("Admin")) { return null; }

                    if (context.Source.UserId != null)
                        return userRepository.GetById((Guid)context.Source.UserId);
                    return null;
                }
            );
        }
    }
}
