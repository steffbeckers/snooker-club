using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;

namespace SC.API.GraphQL.Types
{
    public class LeagueType : ObjectGraphType<League>
    {
        public LeagueType(LeagueRepository leagueRepository, TournamentRepository tournamentRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name).Description("The name of the league");
            Field(x => x.DisplayName).Description("The display name of the league that should be used in app UI");
            Field(x => x.Season, nullable: true);
            Field(x => x.StartDate, nullable: true);
            Field(x => x.EndDate, nullable: true);

            Field<ListGraphType<TournamentType>>(
                "tournaments",
                resolve: context => tournamentRepository.GetByLeagueId(context.Source.Id)
            );

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => leagueRepository.GetPlayersOfLeagueById(context.Source.Id)
            );
        }
    }
}
