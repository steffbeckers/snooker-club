using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Field(x => x.Handicap, nullable: true).Description("The handicap value of the player within league, tournament or frame");

            // TODO: Add authorization on user retrieval?
            Field(x => x.UserId, type: typeof(IdGraphType), nullable: true);
            Field<UserType>(
                "user",
                resolve: context => {
                    if (context.Source.UserId != null)
                        return userRepository.GetById((Guid)context.Source.UserId);
                    return null;
                }
            );
        }
    }
}
