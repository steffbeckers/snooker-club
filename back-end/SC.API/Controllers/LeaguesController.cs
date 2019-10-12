using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC.API.DAL;
using SC.API.Models;
using SC.API.ViewModels;

namespace SC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly SCContext context;
        private readonly IMapper mapper;

        public LeaguesController(SCContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueVM>>> GetLeague()
        {
            return this.mapper.Map<List<LeagueVM>>(await context.Leagues.ToListAsync());
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueVM>> GetLeague(Guid id)
        {
            var league = await context.Leagues.FindAsync(id);

            if (league == null)
            {
                return NotFound();
            }

            return mapper.Map<LeagueVM>(league);
        }

        // PUT: api/Leagues/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(Guid id, League league)
        {
            if (id != league.Id)
            {
                return BadRequest();
            }

            context.Entry(league).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Leagues
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(League league)
        {
            context.Leagues.Add(league);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetLeague", new { id = league.Id }, league);
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<League>> DeleteLeague(Guid id)
        {
            var league = await context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            context.Leagues.Remove(league);
            await context.SaveChangesAsync();

            return league;
        }

        private bool LeagueExists(Guid id)
        {
            return context.Leagues.Any(e => e.Id == id);
        }
    }
}
