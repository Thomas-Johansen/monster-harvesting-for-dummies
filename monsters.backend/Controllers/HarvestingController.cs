using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using monsters.backend.Models;

namespace monsters.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestingController : ControllerBase
    {
        private Component testcomponent = new Component(){Name = "Eye"};



        [HttpGet]
        public string GetComponent()
        {
            return (testcomponent.Name + " has DC ");
        }
    }
}
