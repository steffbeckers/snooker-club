using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class PlayerBLL
    {
        private readonly PlayerRepository playerRepository;

        public PlayerBLL(PlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            return await this.playerRepository.InsertAsync(player);
        }

        public async Task<Player> UpdatePlayerAsync(Guid id, Player playerUpdate)
        {
            // Retrieve existing
            Player player = await this.playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return null;
            }

            // Mapping
            player.FirstName = playerUpdate.FirstName;
            player.LastName = playerUpdate.LastName;

            return await this.playerRepository.UpdateAsync(player);
        }

        public async Task<bool> RemovePlayerAsync(Guid id)
        {
            // Retrieve existing
            Player player = await this.playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return true;
            }

            await this.playerRepository.DeleteAsync(player);

            return true;
        }
    }
}
