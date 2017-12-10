using System;
using System.Linq;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;
using SovaWebService.Models;

namespace SovaWebService.Controllers
{

    [Route("api/questions")]
    public class QuestionsController : Controller
    {

        private readonly IDataService _dataService;

        public QuestionsController(IDataService dataService )
        {
            _dataService = dataService;
        }
        

        [HttpGet("{id}", Name = nameof(GetPost))]
        public IActionResult GetPost(int id)
        {
            var post = _dataService.ReturnQuestionById(id);
            
            var result = new
            {
                Link = Url.Link(nameof(GetPost), new { post.id }),
                post.title,
                post.score,
                post.body,
                Answers = Url.Link(nameof(GetAnswers), new { post.id }),
                
            };

            return Ok(result);

        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswers))]
        public IActionResult GetAnswers(int id)
        {

            var comment = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPost), new { x.id }),
                    Parent = Url.Link(nameof(GetPost), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });


            return Ok(comment);
        }


        //[HttpGet("{searchText}", Name = nameof(GetResults))]
        //public IActionResult GetResults(string searchText, int page = 0, int pageSize = 10)
        //{

        //    CheckPageSize(ref pageSize);

        //    var total = _dataService.GetNumberOfResults(searchText);
        //    var pages = GetTotalPages(pageSize, total);

        //    var data = _dataService.FindQuestionByString(searchText, page, pageSize)
        //        .Select(x => new SearchResultsModel
        //        {
        //            Url = Url.Link(nameof(GetPost), new { x.id }),
        //            title = x.title,
        //            body = x.body,
        //            id = x.id

        //        });


        //    var result = new
        //    {
        //        total,
        //        pages,
        //        prev = Link(nameof(GetResults), page, pageSize, -1, () => page > 0),
        //        next = Link(nameof(GetResults), page, pageSize, 1, () => page < total - 1),
        //        items = data
        //    };

        //    return Ok(result);


        //}



        // GET: api/Questions/java
        //[HttpGet("{searchText}", Name = nameof(GetResults))]
        //public IActionResult GetResults(string searchText, int page = 0, int pageSize = 10)
        //{

        //    CheckPageSize(ref pageSize);

        //    var total = _dataService.GetNumberOfResults(searchText);
        //    var pages = GetTotalPages(pageSize, total);

        //    var data = _dataService.FindQuestionByString(searchText, page, pageSize)
        //        .Select(x => new
        //        {
        //            Link = Url.Link(nameof(GetPost), new { x.id }),
        //            title = x.title
        //            //body = x.body

        //        });


        //    var result = new
        //    {
        //        total,
        //        pages,
        //        prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
        //        next = Link(nameof(GetPost), page, pageSize, 1, () => page < total - 1),
        //        items = data
        //    };

        //    return Ok(result);


        //}

        // GET: api/Questions

        [HttpGet(Name = nameof(GetPosts))]
        public IActionResult GetPosts(int page = 0, int pageSize = 10)
        {

            CheckPageSize(ref pageSize);

            var total = _dataService.GetNumberOfResults("");
            var pages = GetTotalPages(pageSize, total);

            var data = _dataService.FindQuestionByString("", page, pageSize)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPost), new { x.id }),
                    title = x.title

                });


            var result = new
            {
                total,
                pages,
                prev = Link(nameof(GetPost), page, pageSize, -1, () => page > 0),
                next = Link(nameof(GetPost), page, pageSize, 1, () => page < total - 1),
                items = data
            };

            return Ok(result);

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


    }
}
