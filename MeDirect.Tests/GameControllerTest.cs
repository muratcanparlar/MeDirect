using MeDirect.Api;
using MeDirect.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeDirect.Tests
{

    [Collection("Sequential")]
    public class GameControllerTest : IClassFixture<InMemoryWebApplicationFactory<Startup>>
    {
        private InMemoryWebApplicationFactory<Startup> _factory;

        public GameControllerTest(InMemoryWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task web_api_is_healty_returnSuccess()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Game/GameSettings");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task update_game_size_returnSuccess()
        {
            var setting = new GameSetting
            {
                Id = System.Guid.Parse("12b668a7-9dff-4832-a769-6605e717e192"),
                Size = 7

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/update", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task get_game_size_returnSuccesValue()
        {
            var client = _factory.CreateClient();
            var response2 = await client.GetAsync("/api/game/GameSettings");

            var strinResult = await response2.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<GameSetting>(strinResult);
            Assert.Equal("7", jsonObject.Size.ToString());
        }

        [Fact]
        public async Task add_open_game_light_returnSuccess()
        {
            var setting = new GameLight
            {
                Id = new System.Guid(),
                GameSettingId = System.Guid.Parse("12b668a7-9dff-4832-a769-6605e717e192"),
                LightOpenY = 0,
                LightOpenX = 0

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/AddTurnOnLigths", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


       

        [Fact]
        public async Task click_gameboard_and_return_not_complated()
        {

            // Assign
            List<BoardRow> gameBoard = new List<BoardRow>();

            List<BoardCol> x0 = new List<BoardCol>
            {
                new BoardCol{col=true},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y0 = new BoardRow { Columns=x0};

            List<BoardCol> x1 = new List<BoardCol>
            {
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y1 = new BoardRow { Columns = x1 };

            List<BoardCol> x2 = new List<BoardCol>
            {
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=true},
                new BoardCol{col=false}
            };
            BoardRow y2 = new BoardRow { Columns = x2 };

            List<BoardCol> x3 = new List<BoardCol>
            {
                new BoardCol{col=true},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y3 = new BoardRow { Columns = x3 };
            gameBoard.Add(y0);
            gameBoard.Add(y1);
            gameBoard.Add(y2);
            gameBoard.Add(y3);

            GameBoardClick postModel = new GameBoardClick
            {
                BoardRows = gameBoard,
                ClickY = 0,
                ClickX = 3,
                IsBoardComplated = false
            };

            // Act
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");
            var response=  await client.PostAsync("/api/game/ClickBoard",httpContent);

            var strinResult2 = await response.Content.ReadAsStringAsync();
            var jsonObject2 = JsonConvert.DeserializeObject<GameBoardClick>(strinResult2);
            //assert
            Assert.False(jsonObject2.IsBoardComplated);
            
        }

        [Fact]
        public async Task click_gameboard_and_return_complated()
        {

            //Assign
            List<BoardRow> gameBoard = new List<BoardRow>();

            List<BoardCol> x0 = new List<BoardCol>
            {
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y0 = new BoardRow { Columns = x0 };

            List<BoardCol> x1 = new List<BoardCol>
            {
                new BoardCol{col=true},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y1 = new BoardRow { Columns = x1 };

            List<BoardCol> x2 = new List<BoardCol>
            {
                new BoardCol{col=true},
                new BoardCol{col=true},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y2 = new BoardRow { Columns = x2 };

            List<BoardCol> x3 = new List<BoardCol>
            {
                new BoardCol{col=true},
                new BoardCol{col=false},
                new BoardCol{col=false},
                new BoardCol{col=false}
            };
            BoardRow y3 = new BoardRow { Columns = x3 };
            gameBoard.Add(y0);
            gameBoard.Add(y1);
            gameBoard.Add(y2);
            gameBoard.Add(y3);

            GameBoardClick postModel = new GameBoardClick
            {
                BoardRows = gameBoard,
                ClickY = 2,
                ClickX = 0,
                IsBoardComplated = false
            };
            //Act
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/ClickBoard", httpContent);

            var strinResult2 = await response.Content.ReadAsStringAsync();
            var jsonObject2 = JsonConvert.DeserializeObject<GameBoardClick>(strinResult2);
            //assert
            Assert.True(jsonObject2.IsBoardComplated);

        }


        [Fact]
        public async Task delete_gamesetting_returnSuccess()
        {
            var setting = new GameSetting
            {
                Id = System.Guid.Parse("12b668a7-9dff-4832-a769-6605e717e192"),
                Size = 4

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/delete", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task add_gamesetting_returnSuccess()
        {
            var setting = new GameSetting
            {
                Id = System.Guid.Parse("51702bc4-681f-42d3-927e-f995147df6be"),
                Size = 8

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/create", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


      

    }
}
