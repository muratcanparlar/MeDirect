using MeDirect.Core.Models;
using MeDirect.Core.Services;
using MeDirect.Web.Clients;
using MeDirect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameServiceClient _gameServiceClient;
        public HomeController(ILogger<HomeController> logger, IGameServiceClient gameServiceClient)
        {
            _logger = logger;
            _gameServiceClient = gameServiceClient;
        }


        public async Task<List<BoardRow>> CreateBoard()
        {
            var result= await _gameServiceClient.DrawGameBoard();
            List<BoardRow> dataBoardRow = new List<BoardRow>();
            if(result.Content != null)
            {
                dataBoardRow = result.Content.ToList();
            }
            return dataBoardRow;
        }

        [HttpPost]
        public async Task<JsonResult> ClickBoard(GameBoardClick model)
        {
            var result = await _gameServiceClient.ClickBoard(model);
            if (result.Content !=null)
            {
                model.BoardRows = result.Content.BoardRows;
                model.IsBoardComplated = result.Content.IsBoardComplated;
            }
            return Json(new { boardRows = model.BoardRows, isComplated = model.IsBoardComplated });
        }

        
        public async Task<IActionResult> Index()
        {
            var result = await _gameServiceClient.GameSettings();
            UpdateGameSettingsViewModel model = new UpdateGameSettingsViewModel();
            if (result.Content != null)
            {
                model.GameSettings = result.Content;
            }
            return View(model);
        }

        public async Task<IActionResult> Settings()
        {
            var result = await _gameServiceClient.GameSettings();
            UpdateGameSettingsViewModel model = new UpdateGameSettingsViewModel();
            if (result.Content != null)
            {
                model.GameSettings = result.Content;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UpdateGameSettingsViewModel model)
        {
            int resultCode = 0;
            string resultMsg = "";
            var result = await _gameServiceClient.UpdateGameSettings(model.GameSettings);
            if (result.IsSuccessStatusCode)
            {
                resultCode = 0;
                resultMsg = "Game Settings have updated successfully.";

            }
            else
            {
                resultCode = 1;
                resultMsg = "Something went wrong during update process.";
            }
            return Json(new { code = resultCode, msg = resultMsg });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
