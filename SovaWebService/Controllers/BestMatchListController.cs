using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/BestMatchList")]
    public class BestMatchListController : Controller
    {
        private IDataService _dataService;

        public BestMatchListController(IDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/BestMatchList/{searchText}
        [HttpGet("{searchText}", Name = nameof(GetResultsBestMatchList))]
        public IActionResult GetResultsBestMatchList(string searchText)
        {


            if (_dataService.BestmatchKeywordLists(searchText).Count > 0)
            {
                return Ok(_dataService.BestmatchKeywordLists(searchText).Take(20));

            }
            else
            {
                return NotFound();
            }

        }

    }
}