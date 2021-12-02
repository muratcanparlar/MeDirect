using MeDirect.Core.Models;
using MeDirect.Core.Services;
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
        private readonly IGameBoardService _gameBoardService;
        public HomeController(ILogger<HomeController> logger, IGameBoardService gameBoardService)
        {
            _logger = logger;
            _gameBoardService = gameBoardService;
        }


        public List<BoardRow> CreateBoard()
        {
            List<BoardRow> rowData = new List<BoardRow>();
            rowData= _gameBoardService.CreateGameBoard(5, 5);            
            return rowData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
