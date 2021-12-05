using MeDirect.Core.Models;
using MeDirect.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameBoardService _gameBoardService;
        public GameController(IGameBoardService gameBoardService)
        {
            _gameBoardService = gameBoardService;
        }
        [HttpGet("GameSettings")]
        public async Task<ActionResult<IEnumerable<GameSetting>>> GameSettings()
        {
            var apps = await _gameBoardService.GetGameSettings();
            return Ok(apps);
        }
                
        [HttpGet("DrawGameBoard")]
        public async Task<ActionResult<IEnumerable<BoardRow>>> DrawGameBoard()
        {
            var apps = await _gameBoardService.DrawGameBoard();
            return Ok(apps.ToArray());
        }

        [HttpPost("Create")]
        public async Task<ActionResult<IEnumerable<GameSetting>>> CreateGameSetting(GameSetting gameSetting)
        {
            var result = await _gameBoardService.CreateGameSetting(gameSetting);
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<IEnumerable<GameSetting>>> UpdateGameSetting(GameSetting gameSetting)
        {
            await _gameBoardService.UpdateGameSetting(gameSetting);
            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<IEnumerable<GameSetting>>> DeleteGameSetting(GameSetting gameSetting)
        {
            await _gameBoardService.DeleteGameSetting(gameSetting.Id);
            return Ok();
        }

        [HttpPost("ClickBoard")]
        public ActionResult<GameBoardClick> ClickBoard(GameBoardClick gameBoardClick)
        {
           var result= _gameBoardService.ClickBoard(gameBoardClick);
           return Ok(result);
        }


        [HttpGet("GameTurnOnLigths")]
        public async Task<ActionResult<IEnumerable<GameLight>>> GameTurnOnLigths(Guid GameSettingId)
        {
            var apps = await _gameBoardService.GameTurnOnLigths(GameSettingId);
            return Ok(apps);
        }

        [HttpPost("AddTurnOnLigths")]
        public async Task<ActionResult<GameLight>> AddTurnOnLigths(GameLight gameLight)
        {
            var apps = await _gameBoardService.AddTurnOnLigth(gameLight);
            return Ok(apps);
        }

        [HttpDelete("DeleteTurnOnLigths")]
        public async Task<ActionResult<GameLight>> DeleteTurnOnLigths(Guid gameLightId)
        {
            await _gameBoardService.DeleteTurnOnLigth(gameLightId);
            return Ok();
        }
    }
}
