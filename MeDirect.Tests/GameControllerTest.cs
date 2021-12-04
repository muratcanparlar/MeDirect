using MeDirect.Api;
using MeDirect.Core.Models;
using Newtonsoft.Json;
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
        public async Task web_api_is_healty()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Game/GameSettings");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
