using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{
    [Route("api/visited")]
    public class VisitedController : Controller
    {

        private IDataService _dataService;

        public VisitedController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet(Name = nameof(GetPostsVisited))]
        public IActionResult GetPostsVisited()
        {

            var data = _dataService.ReturnVisitedPosts()
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostVisited), new { x.id }),
                    title = x.title,
                    body = x.body.Substring(0, x.body.Length < 300 ? x.body.Length : 300),
                    date = x.view_date
                });


            var result = new
            {
                items = data
            };

            return Ok(result);

        }


        [HttpGet("{id}", Name = nameof(GetPostVisited))]
        public IActionResult GetPostVisited(int id)
        {
            var post = _dataService.ReturnQuestionById(id);
            //add visited 
            _dataService.AddVisitedPost(id);
            var result = new
            {
                Link = Url.Link(nameof(GetPostVisited), new { post.id }),
                //post.title,
                post.score,
                post.body,
                post.creation_date,
                answers = Url.Link(nameof(GetAnswersVisited), new { post.id }),
                comments = Url.Link(nameof(GetCommentsVisited), new { post.id }),

            };

            return Ok(result);

        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswersVisited))]
        public IActionResult GetAnswersVisited(int id)
        {
            var answers = _dataService.ReturnAnswersById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostVisited), new { x.id }),
                    Parent = Url.Link(nameof(GetPostVisited), new { id }),
                    body = x.body,
                    x.creation_date,
                    x.score
                });

            return Ok(answers);
        }


        [HttpGet("{id}/comments", Name = nameof(GetCommentsVisited))]
        public IActionResult GetCommentsVisited(int id)
        {

            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetCommentsVisited), new { x.id }),
                    Parent = Url.Link(nameof(GetCommentsVisited), new { id }),
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


    }
}