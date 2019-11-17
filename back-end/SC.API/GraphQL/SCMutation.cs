using GraphQL.Types;
using SC.API.BLL;
using SC.API.GraphQL.Types;
using SC.API.Models;
using System;

namespace SC.API.GraphQL
{
    public class SCMutation : ObjectGraphType
    {
        public SCMutation(
            LeagueBLL leagueBLL,
            PlayerBLL playerBLL,
            SettingBLL settingBLL,
            TournamentBLL tournamentBLL,
            FrameBLL frameBLL
        )
        {
            #region Leagues

            FieldAsync<LeagueType>(
                "createLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeagueInputType>>
                    {
                        Name = "league"
                    }
                ),
                resolve: async context =>
                {
                    League league = context.GetArgument<League>("league");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.CreateLeagueAsync(league)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "updateLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<LeagueInputType>>
                    {
                        Name = "league"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    League league = context.GetArgument<League>("league");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.UpdateLeagueAsync(id, league)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "linkPlayerToLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeaguePlayerInputType>>
                    {
                        Name = "leaguePlayer"
                    }
                ),
                resolve: async context =>
                {
                    LeaguePlayer leaguePlayer = context.GetArgument<LeaguePlayer>("leaguePlayer");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.LinkPlayerToLeagueAsync(leaguePlayer)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "unlinkPlayerFromLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeaguePlayerInputType>>
                    {
                        Name = "leaguePlayer"
                    }
                ),
                resolve: async context =>
                {
                    LeaguePlayer leaguePlayer = context.GetArgument<LeaguePlayer>("leaguePlayer");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.UnlinkPlayerFromLeagueAsync(leaguePlayer)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.RemoveLeagueAsync(id)
                    );
                }
            );

            #endregion

            #region Players

            FieldAsync<PlayerType>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>>
                    {
                        Name = "player"
                    }
                ),
                resolve: async context =>
                {
                    Player player = context.GetArgument<Player>("player");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.CreatePlayerAsync(player)
                    );
                }
            );

            FieldAsync<PlayerType>(
                "updatePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<PlayerInputType>>
                    {
                        Name = "player"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Player player = context.GetArgument<Player>("player");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.UpdatePlayerAsync(id, player)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.RemovePlayerAsync(id)
                    );
                }
            );

            #endregion

            #region Settings

            FieldAsync<SettingType>(
                "createSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SettingInputType>>
                    {
                        Name = "setting"
                    }
                ),
                resolve: async context =>
                {
                    Setting setting = context.GetArgument<Setting>("setting");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.CreateSettingAsync(setting)
                    );
                }
            );

            FieldAsync<SettingType>(
                "updateSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<SettingInputType>>
                    {
                        Name = "setting"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Setting setting = context.GetArgument<Setting>("setting");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.UpdateSettingAsync(id, setting)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.RemoveSettingAsync(id)
                    );
                }
            );

            #endregion

            #region Tournaments

            FieldAsync<TournamentType>(
                "createTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<TournamentInputType>>
                    {
                        Name = "tournament"
                    }
                ),
                resolve: async context =>
                {
                    Tournament tournament = context.GetArgument<Tournament>("tournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.CreateTournamentAsync(tournament)
                    );
                }
            );

            FieldAsync<TournamentType>(
                "updateTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<TournamentInputType>>
                    {
                        Name = "tournament"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Tournament tournament = context.GetArgument<Tournament>("tournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.UpdateTournamentAsync(id, tournament)
                    );
                }
            );

            FieldAsync<TournamentType>(
                "linkPlayerToTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerTournamentInputType>>
                    {
                        Name = "playerTournament"
                    }
                ),
                resolve: async context =>
                {
                    PlayerTournament playerTournament = context.GetArgument<PlayerTournament>("playerTournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.LinkPlayerToTournamentAsync(playerTournament)
                    );
                }
            );

            FieldAsync<TournamentType>(
                "unlinkPlayerFromTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerTournamentInputType>>
                    {
                        Name = "playerTournament"
                    }
                ),
                resolve: async context =>
                {
                    PlayerTournament playerTournament = context.GetArgument<PlayerTournament>("playerTournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.UnlinkPlayerFromTournamentAsync(playerTournament)
                    );
                }
            );

            FieldAsync<TournamentType>(
                "linkPlayerPositionToTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerPositionTournamentInputType>>
                    {
                        Name = "playerPositionTournament"
                    }
                ),
                resolve: async context =>
                {
                    PlayerPositionTournament playerPositionTournament = context.GetArgument<PlayerPositionTournament>("playerPositionTournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.LinkPlayerPositionToTournamentAsync(playerPositionTournament)
                    );
                }
            );

            FieldAsync<TournamentType>(
                "unlinkPlayerPositionFromTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerPositionTournamentInputType>>
                    {
                        Name = "playerPositionTournament"
                    }
                ),
                resolve: async context =>
                {
                    PlayerPositionTournament playerPositionTournament = context.GetArgument<PlayerPositionTournament>("playerPositionTournament");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.UnlinkPlayerPositionFromTournamentAsync(playerPositionTournament)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeTournament",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await tournamentBLL.RemoveTournamentAsync(id)
                    );
                }
            );

            #endregion

            #region Frames

            FieldAsync<FrameType>(
                "createFrame",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FrameInputType>>
                    {
                        Name = "frame"
                    }
                ),
                resolve: async context =>
                {
                    Frame frame = context.GetArgument<Frame>("frame");

                    return await context.TryAsyncResolve(
                        async c => await frameBLL.CreateFrameAsync(frame)
                    );
                }
            );

            FieldAsync<FrameType>(
                "updateFrame",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<FrameInputType>>
                    {
                        Name = "frame"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Frame frame = context.GetArgument<Frame>("frame");

                    return await context.TryAsyncResolve(
                        async c => await frameBLL.UpdateFrameAsync(id, frame)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeFrame",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await frameBLL.RemoveFrameAsync(id)
                    );
                }
            );

            #endregion
        }
    }
}
