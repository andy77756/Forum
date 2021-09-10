using ForumDAL.Models;
using ForumLib.Models;
using ForumLib.Services.ForumService;
using ForumWebApi.Filters;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IForumService ForumService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForumService forumService)
        {
            _logger = logger;
            ForumService = forumService;
        }

        [TypeFilter(typeof(LevelOneAuthorizationFilter))]
        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(new Result<IEnumerable<WeatherForecast>>(1, result));
        }

    }
}
