using MeDirect.Core.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.Web.Clients
{
    public interface IGameServiceClient
    {
        [Get("/Game/GameSettings")]
        public Task<ApiResponse<GameSetting>> GameSettings();

        [Get("/Game/DrawGameBoard")]
        public Task<ApiResponse<IEnumerable<BoardRow>>> DrawGameBoard();

        [Post("/Game/Update/")]
        public Task<ApiResponse<GameSetting>> UpdateGameSettings([Body] GameSetting gameSetting);

        [Post("/Game/ClickBoard/")]
        public Task<ApiResponse<GameBoardClick>> ClickBoard([Body] GameBoardClick GameBoardClick);

        [Get("/Game/GameTurnOnLigths")]
        public Task<ApiResponse<IEnumerable<GameLight>>> GameTurnOnLigths(Guid GameSettingId);

        [Post("/Game/AddTurnOnLigths")]
        public Task<ApiResponse<IEnumerable<GameLight>>> AddTurnOnLigths(GameLight model);

        [Delete("/Game/DeleteTurnOnLigths")]
        public Task<ApiResponse<IEnumerable<GameLight>>> DeleteTurnOnLigths(Guid gameLightId);

    }
}
