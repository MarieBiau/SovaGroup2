using System;
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
            var returnSearches = _dataService.ReturnSearches()
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostSearches), new { x.text }),
                    text = x.text,
                     date = x.search_date

                }).Take(5);

            var result = new
            {
                items = returnSearches
            };

            return Ok(result);
        }


        [HttpGet("{searchText}", Name = nameof(GetPostSearches))]
        public IActionResult GetPostSearches(string searchText, int page = 0, int pageSize = 10)
        {


            var post = _dataService.BestMatches(searchText, page, pageSize);

            var result = new
            {
                Link = Url.Link(nameof(GetPostSearches), new { post[0].id }),
                //post.title,
                post[0].rank,
                post[0].body,
                answers = Url.Link(nameof(GetAnswersSearches), new { post[0].id }),
                comments = Url.Link(nameof(GetCommentsSearches), new { post[0].id }),

            };

            return Ok(result);


        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswersSearches))]
        public IActionResult GetAnswersSearches(int id)
        {
            var answers = _dataService.ReturnAnswersById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostSearches), new { x.id }),
                    Parent = Url.Link(nameof(GetPostSearches), new { id }),
                    body = x.body,
                    x.creation_date,
                    x.score
                });

            return Ok(answers);
        }


        [HttpGet("{id}/comments", Name = nameof(GetCommentsSearches))]
        public IActionResult GetCommentsSearches(int id)
        {

            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetCommentsSearches), new { x.id }),
                    Parent = Url.Link(nameof(GetCommentsSearches), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });

            return Ok(comments);
        }


        // Helpers 

        private string Link(string route, int page, int pageSize, int pageInc = 0, Func<bool> f = null)
        {
            if (f == null) return Url.Link(route, new { page, pageSize });

            return f()
                ? Url.Link(route, new { page = page + pageInc, pageSize })
                : null;
        }

        private static int GetTotalPages(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize);
        }

        private static void CheckPageSize(ref int pageSize)
        {
            pageSize = pageSize > 50 ? 50 : pageSize;
        }

        // GET: api/searches/if 0 means take all
        //[HttpGet("{amount}")]
        //public IActionResult Get(int amount)
        //{
        //    if (_dataService.ReturnSearches().Count > 0)
        //    {
        //        //return latest top 10 or other amount requested by client
        //        return Ok(_dataService.ReturnSearches().Take(amount));
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}


    }
}
