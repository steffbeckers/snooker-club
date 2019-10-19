using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class LeagueBLL
    {
        private readonly LeagueRepository leagueRepository;

        public LeagueBLL(LeagueRepository leagueRepository)
        {
            this.leagueRepository = leagueRepository;
        }

        public async Task<League> CreateLeagueAsync(League league)
        {
            // Calculated fields
            league.Name = league.DisplayName.Trim().Replace(" ", "-").ToLower();
            if (!string.IsNullOrEmpty(league.Season))
            {
                league.Name += "-" + league.Season.Trim().Replace(" ", "-").ToLower();
            }

            // TODO: Validate duplicate?

            return await this.leagueRepository.InsertAsync(league);
        }

        public async Task<League> UpdateLeagueAsync(Guid id, League leagueUpdate)
        {
            // Retrieve existing
            League league = await this.leagueRepository.GetByIdAsync(id);
            if (league == null)
            {
                return null;
            }

            // Mapping
            league.DisplayName = leagueUpdate.DisplayName;
            league.Season = leagueUpdate.Season;
            league.StartDate = leagueUpdate.StartDate;
            league.EndDate = leagueUpdate.EndDate;

            // Calculated fields
            league.Name = league.DisplayName.Trim().Replace(" ", "-").ToLower();
            if (!string.IsNullOrEmpty(league.Season))
            {
                league.Name += "-" + league.Season.Trim().Replace(" ", "-").ToLower();
            }

            // TODO: Validate duplicate?

            return await this.leagueRepository.UpdateAsync(league);
        }

        public async Task<League> LinkPlayerToLeagueAsync(LeaguePlayer leaguePlayer)
        {
            throw new NotImplementedException();
        }

        public async Task<League> UnlinkPlayerFromLeagueAsync(LeaguePlayer leaguePlayer)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveLeagueAsync(Guid id)
        {
            // Retrieve existing
            League league = await this.leagueRepository.GetByIdAsync(id);
            if (league == null)
            {
                return true;
            }

            await this.leagueRepository.DeleteAsync(league);

            return true;
        }
    }
}
