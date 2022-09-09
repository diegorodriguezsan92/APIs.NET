using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace APIVersionControl.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppID = "631b28f075abde6e76f97c82";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetUsaerData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID);

            var response = await _httpClient.GetStreamAsync(ApiTestURL);

            var usersData = await JsonSerializer.DeserializeAsync<UsersResponseData>(response);

            return Ok(usersData);

        }


    }
}
