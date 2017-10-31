using System.Linq;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{

    [Route("api/searches")]
    public class SearchesController : Controller
    {
        private IDataService _dataService;

        public SearchesController(IDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/searches/if 0 means take all
        [HttpGet]
        public IActionResult Get()
        {
            if (_dataService.ReturnSearches().Count > 0)
            {

                //return latest top 10 or other amount requested by client
                return Ok(_dataService.ReturnSearches());

            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/searches/if 0 means take all
        [HttpGet("{amount}")]
        public IActionResult Get(int amount)
        {
            if (_dataService.ReturnSearches().Count > 0)
            {
                //return latest top 10 or other amount requested by client
                return Ok(_dataService.ReturnSearches().Take(amount));
            }
            else
            {
                return NotFound();
            }
        }

    }
}
