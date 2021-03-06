﻿using GraphQL;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class TournamentBLL
    {
        private readonly TournamentRepository tournamentRepository;
        private readonly PlayerTournamentRepository playerTournamentRepository;
        private readonly PlayerPositionTournamentRepository playerPositionTournamentRepository;

        public TournamentBLL(
            TournamentRepository tournamentRepository,
            PlayerTournamentRepository playerTournamentRepository,
            PlayerPositionTournamentRepository playerPositionTournamentRepository
        )
        {
            this.tournamentRepository = tournamentRepository;
            this.playerTournamentRepository = playerTournamentRepository;
            this.playerPositionTournamentRepository = playerPositionTournamentRepository;
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
            PlayerTournament playerTournamentLink = this.playerTournamentRepository.GetByPlayerAndTournamentId(playerTournament.PlayerId, playerTournament.TournamentId);

            if (playerTournamentLink == null)
            {
                await this.playerTournamentRepository.InsertAsync(playerTournament);
            }
            else
            {
                // Mapping
                playerTournamentLink.Handicap = playerTournament.Handicap;

                await this.playerTournamentRepository.UpdateAsync(playerTournamentLink);
            }

            return this.tournamentRepository.GetWithPlayersById(playerTournament.TournamentId);
        }

        public async Task<Tournament> UnlinkPlayerFromTournamentAsync(PlayerTournament playerTournament)
        {
            PlayerTournament playerTournamentLink = this.playerTournamentRepository.GetByPlayerAndTournamentId(playerTournament.PlayerId, playerTournament.TournamentId);

            if (playerTournamentLink != null)
            {
                await this.playerTournamentRepository.DeleteAsync(playerTournamentLink);

                // Unlink all player positions from tournament as well
                this.playerPositionTournamentRepository.DeleteByPlayerAndTournamentId(playerTournament.PlayerId, playerTournament.TournamentId);

                // Remove player ID's on tournament
                this.RemovePlayerOnTournament(playerTournament.PlayerId, playerTournament.TournamentId);
            }

            return this.tournamentRepository.GetWithPlayersById(playerTournament.TournamentId);
        }

        private void RemovePlayerOnTournament(Guid playerId, Guid tournamentId)
        {
            Tournament tournament = this.tournamentRepository.GetById(tournamentId);
            if (tournament == null) { return; }

            if (tournament.WinnerId == playerId)
            {
                tournament.WinnerId = null;
            }

            if (tournament.RunnerUpId == playerId)
            {
                tournament.RunnerUpId = null;
            }

            this.tournamentRepository.Update(tournament);
        }

        public async Task<Tournament> LinkPlayerPositionToTournamentAsync(PlayerPositionTournament playerPositionTournament)
        {
            // Validation
            if (playerPositionTournament.PlayerId == Guid.Empty)
            {
                throw new ExecutionError("TournamentBLL.LinkPlayerPositionToTournamentAsync(PlayerPositionTournament playerPositionTournament) => playerPositionTournament.PlayerId must be provided.");
            }

            // First remove existing link for position
            PlayerPositionTournament playerPositionTournamentLink = this.playerPositionTournamentRepository.GetByPositionAndTournamentId(playerPositionTournament.Position, playerPositionTournament.TournamentId);
            
            if (playerPositionTournamentLink != null)
            {
                await this.playerPositionTournamentRepository.DeleteAsync(playerPositionTournamentLink);
            }

            // Add new link for position
            await this.playerPositionTournamentRepository.InsertAsync(playerPositionTournament);

            return this.tournamentRepository.GetWithPlayerPositionsById(playerPositionTournament.TournamentId);
        }

        public async Task<Tournament> UnlinkPlayerPositionFromTournamentAsync(PlayerPositionTournament playerPositionTournament)
        {
            PlayerPositionTournament playerPositionTournamentLink = this.playerPositionTournamentRepository.GetByPositionAndTournamentId(playerPositionTournament.Position, playerPositionTournament.TournamentId);

            if (playerPositionTournamentLink != null)
            {
                await this.playerPositionTournamentRepository.DeleteAsync(playerPositionTournamentLink);
            }

            return this.tournamentRepository.GetWithPlayerPositionsById(playerPositionTournament.TournamentId);
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
