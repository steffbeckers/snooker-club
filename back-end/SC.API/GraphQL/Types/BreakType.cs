using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class BreakType : ObjectGraphType<Break>
    {
        public BreakType(PlayerRepository playerRepository, FrameRepository frameRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Score);

            Field(x => x.PlayerId, type: typeof(IdGraphType));
            Field<PlayerType>(
                "player",
                resolve: context => playerRepository.GetById(context.Source.PlayerId)
            );

            Field(x => x.FrameId, type: typeof(IdGraphType));
            Field<FrameType>(
                "frame",
                resolve: context => frameRepository.GetById(context.Source.FrameId)
            );
        }
    }
}
