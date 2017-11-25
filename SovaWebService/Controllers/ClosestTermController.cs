using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/ClosestTerm")]
    public class ClosestTermController : Controller
    {

        private IDataService _dataService;

        public ClosestTermController(IDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/BestMatch/{searchText}
        [HttpGet("{searchText}", Name = nameof(GetResultsClosestTerms))]
        public IActionResult GetResultsClosestTerms(string searchText)
        {


            if (_dataService.ClosestTerms(searchText).Count > 0)
            {
                return Ok(_dataService.ClosestTerms(searchText).Take(5));

            }
            else
            {
                return NotFound();
            }

        }









    }
}