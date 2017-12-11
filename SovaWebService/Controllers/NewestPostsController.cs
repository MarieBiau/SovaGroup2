using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/newestposts")]
    public class NewestPostsController : Controller
    {
        private IDataService _dataService;

        public NewestPostsController(IDataService dataService)
        {
            _dataService = dataService;
        }


        // GET: api/searches/if 0 means take all
        [HttpGet(Name = nameof(GetNewestPosts))]
        public IActionResult GetNewestPosts()
        {

            var newestPosts = _dataService.ReturnNewestPosts(10)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetNewestPosts), new { x.id }),
                    title = x.title,
                    body = x.body,
                    date = x.creation_date,
                    score = x.score

                });
            var result = new
            {
                items = newestPosts
            };

            return Ok(result);

        }


        // GET: api/newestposts/if 0 means take all
        [Route("api/newestposts/visited")]
        [HttpGet]
        public IActionResult GetVisited()
        {
            var visitedPagesList = _dataService.ReturnVisitedPosts();

            return Ok(visitedPagesList);
        }


    }


}