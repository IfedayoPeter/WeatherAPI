using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Helpers;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("my-weather-api")]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("/forecasts")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetForecasts([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city)) return BadRequest("city parameter not provided");
            try
            {
                var httpClient = _httpClientFactory.CreateClient(ConfigurationManagerHelper.WeatherHttpApi);
                var endpoint = ConfigurationManagerHelper.AppSettings["WeatherAPI:Paths:Forecast"];
                var key = ConfigurationManagerHelper.AppSettings["WeatherAPI:Key"];
                var path = $"v1{endpoint}?key={key}&q={city}";
                var response = await httpClient.GetAsync(path);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return BadRequest(JsonSerializer.Deserialize<ErrorResponse>(content));
                return Ok(JsonSerializer.Deserialize<ForecastResponse>(content));
            }
            catch (Exception)
            {
                return UnprocessableEntity("Error during request");
            }
        }
        
        [HttpGet("/current")]
        [Authorize(Roles = "Standard, Administrator")]
        public async Task<IActionResult> GetCurrent([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city)) return BadRequest("city parameter not provided");
            try
            {
                var httpClient = _httpClientFactory.CreateClient(ConfigurationManagerHelper.WeatherHttpApi);
                var endpoint = ConfigurationManagerHelper.AppSettings["WeatherAPI:Paths:Current"];
                var key = ConfigurationManagerHelper.AppSettings["WeatherAPI:Key"];
                var path = $"v1{endpoint}?key={key}&q={city}";
                var response = await httpClient.GetAsync(path);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return BadRequest(JsonSerializer.Deserialize<ErrorResponse>(content));
                return Ok(JsonSerializer.Deserialize<CurrentResponse>(content));
            }
            catch (Exception)
            {
                return UnprocessableEntity("Error during request");
            }
        }
    }
}