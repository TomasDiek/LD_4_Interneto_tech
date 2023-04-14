using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LD_4_Interneto_tech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityControler : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Kaunas", "Vilnius" };
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Kaunas";
        }
    }
}
