using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPaises.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value", "value12" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Este es un mensaje {id}";
        }

        [HttpPost]
        public void Post([FromBody] string value) { }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
