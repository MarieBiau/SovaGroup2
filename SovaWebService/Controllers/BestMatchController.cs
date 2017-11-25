using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/BestMatch")]
    public class BestMatchController : Controller
    {
        private IDataService _dataService;

        public BestMatchController(IDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/BestMatch/{searchText}
        [HttpGet("{searchText}", Name = nameof(GetResultsBestMatch))]
        public IActionResult GetResultsBestMatch(string searchText)
        {


            if (_dataService.BestMatches(searchText).Count > 0)
            {
                return Ok(_dataService.BestMatches(searchText).Take(5));

            }
            else
            {
                return NotFound();
            }

        }

        
    }
}
