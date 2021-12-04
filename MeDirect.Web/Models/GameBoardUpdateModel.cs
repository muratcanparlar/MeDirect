using MeDirect.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.Web.Models
{
    public class GameBoardUpdateModel
    {
        public List<BoardRow> BoardRows { get; set; }

        public int ClickX { get; set; }

        public int ClickY { get; set; }

    }
}
