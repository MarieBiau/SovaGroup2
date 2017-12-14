using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/tags")]
    public class PopularTagsController : Controller
    {
        private readonly IDataService _dataService;

        public PopularTagsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/tags/{numberOfMostPopularTags}
        [HttpGet("{numberOfMostPopularTags}", Name = nameof(GetResultsPopularTags))]
        public IActionResult GetResultsPopularTags(int numberOfMostPopularTags)
        {


            if (_dataService.PopularTagsList(numberOfMostPopularTags).Count > 0)
            {
                return Ok(_dataService.PopularTagsList(numberOfMostPopularTags));

            }
            else
            {
                return NotFound();
            }

        }
    }
}
