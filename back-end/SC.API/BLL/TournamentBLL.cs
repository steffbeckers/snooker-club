using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class TournamentBLL
    {
        private readonly TournamentRepository tournamentRepository;

        public TournamentBLL(TournamentRepository tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        public async Task<Tournament> CreateTournamentAsync(Tournament tournament)
        {
            // Validation
            if (string.IsNullOrEmpty(tournament.DisplayName))
            {
                tournament.DisplayName = null;
                tournament.Name = null;
            }

            // Calculated fields
            if (!string.IsNullOrEmpty(tournament.DisplayName))
                tournament.Name = tournament.DisplayName.Trim().Replace(" ", "-").ToLower();

            // TODO: Validate duplicate?

            return await this.tournamentRepository.InsertAsync(tournament);
        }

        public async Task<Tournament> UpdateTournamentAsync(Guid id, Tournament tournamentUpdate)
        {
            // Retrieve existing
            Tournament tournament = await this.tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
            {
                return null;
            }

            // Mapping
            tournament.DisplayName = tournamentUpdate.DisplayName;
            tournament.Date = tournamentUpdate.Date;
            tournament.WinnerId = tournamentUpdate.WinnerId;
            tournament.RunnerUpId = tournamentUpdate.RunnerUpId;
            //tournament.LeagueId = tournamentUpdate.LeagueId; // TODO: Allowed to switch league? What with players

            // Validation
            if (string.IsNullOrEmpty(tournament.DisplayName))
            {
                tournament.DisplayName = null;
                tournament.Name = null;
            }

            // Calculated fields
            if (!string.IsNullOrEmpty(tournament.DisplayName))
                tournament.Name = tournament.DisplayName.Trim().Replace(" ", "-").ToLower();

            // TODO: Validate duplicate?

            return await this.tournamentRepository.UpdateAsync(tournament);
        }

        public async Task<Tournament> LinkPlayerToTournamentAsync(PlayerTournament playerTournament)
        {
            throw new NotImplementedException();
        }

        public async Task<Tournament> UnlinkPlayerFromTournamentAsync(PlayerTournament playerTournament)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveTournamentAsync(Guid id)
        {
            // Retrieve existing
            Tournament tournament = await this.tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
            {
                return true;
            }

            await this.tournamentRepository.DeleteAsync(tournament);

            return true;
        }
    }
}
