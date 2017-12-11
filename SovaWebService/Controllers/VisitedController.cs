using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/Visited")]
    public class VisitedController : Controller
    {

        private IDataService _dataService;

        public VisitedController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetVisited()
        {


            var visitedPagesList = _dataService.ReturnVisitedPosts()
                .Select(x => new
                {
                    title = x.title,
                    body = x.body.Substring(3,100),
                    date = x.view_date,


                });
            var result = new
            {
                items = visitedPagesList
            };

            return Ok(result);
        }


    }
}