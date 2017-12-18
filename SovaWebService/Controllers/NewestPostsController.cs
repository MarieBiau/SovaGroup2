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
                    Link = Url.Link(nameof(GetPostHome), new { x.id }),
                    title = x.title,
                    body = x.body.Substring(0, x.body.Length < 300 ? x.body.Length : 300),
                    date = x.creation_date,
                    //score = x.score

                });
            var result = new
            {
                items = newestPosts
            };

            return Ok(result);

        }

        [HttpGet("{id}", Name = nameof(GetPostHome))]
        public IActionResult GetPostHome(int id)
        {
            var post = _dataService.ReturnQuestionById(id);
            //add visited 
            _dataService.AddVisitedPost(id);
            var result = new
            {
                Link = Url.Link(nameof(GetPostHome), new { post.id }),
                //post.title,
                post.score,
                post.body,
                post.creation_date,
                answers = Url.Link(nameof(GetAnswersHome), new { post.id }),
                comments = Url.Link(nameof(GetCommentsHome), new { post.id }),
                linkedPosts = Url.Link(nameof(GetlinkedPostsHome), new { post.id }),
                showTags = Url.Link(nameof(GetshowTagsHome), new { post.id }),
                addMark = Url.Link(nameof(addMarkHome), new { post.id }),
                removeMark = Url.Link(nameof(removeMarkHome), new { post.id }),

            };

            return Ok(result);

        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswersHome))]
        public IActionResult GetAnswersHome(int id)
        {
            var answers = _dataService.ReturnAnswersById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostHome), new { x.id }),
                    Parent = Url.Link(nameof(GetPostHome), new { id }),
                    comments = Url.Link(nameof(GetCommentsHome), new { x.id }),
                    body = x.body,
                    x.creation_date,
                    x.score
                });

            return Ok(answers);
        }


        [HttpGet("{id}/comments", Name = nameof(GetCommentsHome))]
        public IActionResult GetCommentsHome(int id)
        {

            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetCommentsHome), new { x.id }),
                    Parent = Url.Link(nameof(GetCommentsHome), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });

            return Ok(comments);
        }

        [HttpGet("{id}/linkedPosts", Name = nameof(GetlinkedPostsHome))]
        public IActionResult GetlinkedPostsHome(int id)
        {

            var linkedPosts = _dataService.ReturnLinkPosts(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetlinkedPostsHome), new { x.id }),
                    Parent = Url.Link(nameof(GetlinkedPostsHome), new { id }),
                    title = x.title
                });

            return Ok(linkedPosts);
        }

        [HttpGet("{id}/showTags", Name = nameof(GetshowTagsHome))]
        public IActionResult GetshowTagsHome(int id)
        {

            var linkedPosts = _dataService.ReturnPostTags(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetshowTagsHome), new { x.id }),
                    Parent = Url.Link(nameof(GetshowTagsHome), new { id }),
                    name = x.name
                });

            return Ok(linkedPosts);
        }

        [HttpGet("{id}/addMark", Name = nameof(addMarkHome))]
        public IActionResult addMarkHome(int id)
        {
            var checkIfMarkExist = _dataService.GetMark(id);

            if (checkIfMarkExist != null)
            {

                return Ok();
            }
            else
            {
                var MarkPost = _dataService.MarkPost(id, 1);
                return Ok(MarkPost);

            }

        }

        [HttpGet("{id}/removeMark", Name = nameof(removeMarkHome))]
        public IActionResult removeMarkHome(int id)
        {

            var MarkPost = _dataService.DeleteMarkPost(id);
            return Ok(MarkPost);


        }


        // GET: api/newestposts/if 0 means take all
        [Route("api/newestposts/visited")]
        [HttpGet]
        public IActionResult GetVisited()
        {
            var visitedPagesList = _dataService.ReturnVisitedPosts();

            return Ok(visitedPagesList);
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