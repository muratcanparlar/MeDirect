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
    public class ProductsControllerTest : IClassFixture<InMemoryWebApplicationFactory<Startup>>
    {
        private InMemoryWebApplicationFactory<Startup> _factory;

        public ProductsControllerTest(InMemoryWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task delete_request_test()
        {
            var setting = new GameSetting
            {
                Id = System.Guid.Parse("12b668a7-9dff-4832-a769-6605e717e192"),
                Size = 6

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/delete", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task post_request_test()
        {
            var setting = new GameSetting
            {
                Id = System.Guid.Parse("12b668a7-9dff-4832-a769-6605e717e192"),
                Size = 8

            };
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/game/create", httpContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }



        [Fact]
        public async Task update_game_size_request_test()
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
        public async Task get_game_size_request_test()
        {
            var client = _factory.CreateClient();
            var response2 = await client.GetAsync("/api/game/GameSettings");

            var strinResult = await response2.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<GameSetting>(strinResult);
            Assert.Equal("7", jsonObject.Size.ToString());
        }


        [Fact]
        public async Task draw_gameboard_request_test()
        {
            var client = _factory.CreateClient();
            var response2 = await client.GetAsync("/api/game/DrawGameBoard");

            var strinResult = await response2.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<IEnumerable<BoardRow>>(strinResult);
            List<BoardRow> asList = jsonObject.ToList();
            Assert.Equal(6 , asList.Count);
        }

        [Fact]
        public async Task click_gameboard_request_test()
        {

            //setup
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
            //
            var client = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");
            var response=  await client.PostAsync("/api/game/ClickBoard",httpContent);

            var strinResult2 = await response.Content.ReadAsStringAsync();
            var jsonObject2 = JsonConvert.DeserializeObject<GameBoardClick>(strinResult2);
            //assert
            Assert.False(jsonObject2.IsBoardComplated);
            
        }


        [Fact]
        public async Task web_api_is_healty()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Game/GameSettings");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
