using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using monsters.backend.Models;

namespace monsters.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestingController : ControllerBase
    {
        private Component testcomponent = new Component(){Name = "Eye", isCraftingMaterial = true, isEdible = true};



        [HttpGet]
        public string GetComponent()
        {
            return (testcomponent.Name + " has DC ");
        }




        private List<Component> PerformHarvesting(List<Component> components, CreatureType creatureType , int totalScore)
        {
            List<Component> result = new List<Component>();
            int totalDC;
            //TODO: Figure out how to get the correct DC for the Component here...
            //Either get from front end or get from database
            //The frontend will need to get it from database to have in the first place...
            //Alternatively the format on the input data needs to be different, and not just a generic list of the components
            foreach (Component component in components)
            {
                
            } 
            
            
            return result;
        }
    }
}
