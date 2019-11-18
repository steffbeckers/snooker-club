using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Threading.Tasks;

namespace SC.API.BLL
{
    public class FrameBLL
    {
        private readonly FrameRepository frameRepository;

        public FrameBLL(FrameRepository frameRepository)
        {
            this.frameRepository = frameRepository;
        }

        public async Task<Frame> CreateFrameAsync(Frame frame)
        {
            return await this.frameRepository.InsertAsync(frame);
        }

        public async Task<Frame> UpdateFrameAsync(Guid id, Frame frameUpdate)
        {
            // Retrieve existing
            Frame frame = await this.frameRepository.GetByIdAsync(id);
            if (frame == null)
            {
                return null;
            }

            // Mapping
            frame.StartDate = frameUpdate.StartDate;
            frame.EndDate = frameUpdate.EndDate;
            frame.TournamentPhase = frameUpdate.TournamentPhase;
            frame.TournamentId = frameUpdate.TournamentId;
            frame.GroupId = frameUpdate.GroupId;
            frame.WinnerId = frameUpdate.WinnerId;

            return await this.frameRepository.UpdateAsync(frame);
        }

        public async Task<bool> RemoveFrameAsync(Guid id)
        {
            // Retrieve existing
            Frame frame = await this.frameRepository.GetByIdAsync(id);
            if (frame == null)
            {
                return true;
            }

            await this.frameRepository.DeleteAsync(frame);

            return true;
        }
    }
}
