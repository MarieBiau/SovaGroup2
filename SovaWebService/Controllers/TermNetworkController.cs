using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/graph")]
    public class TermNetworkController : Controller
    {
        private IDataService _dataService;

        public TermNetworkController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/graph/{searchText}
        [HttpGet("{searchText}", Name = nameof(GetTermNetworkResult))]
        public IActionResult GetTermNetworkResult(string searchText)
        {


            if (_dataService.TermNetworkList(searchText).Count > 0)
            {
                return Ok(_dataService.TermNetworkList(searchText));

            }
            else
            {
                return NotFound();
            }

        }
    }
}
