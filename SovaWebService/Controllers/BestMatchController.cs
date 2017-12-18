using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SovaWebService.Models;

namespace SovaWebService.Controllers
{
    [Route("api/BestMatch")]
    public class BestMatchController : Controller
    {

        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public BestMatchController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        // GET: api/BestMatch/search/{searchText}
        [Route("custom")]
        [HttpGet("search/{searchText}", Name = nameof(GetResultsOfBestMatch))]
        public IActionResult GetResultsOfBestMatch(string searchText, int page = 0, int pageSize = 10)
        {

            CheckPageSize(ref pageSize);

            var total = _dataService.BestMatchesTotal(searchText);
            var pages = GetTotalPages(pageSize, total);

            var data = _dataService.BestMatches(searchText, page, pageSize)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostBestMatch), new { x.id }),
                    title = x.title,
                    body = x.body.Substring(0, x.body.Length < 300 ? x.body.Length : 300),
                    creationDate = x.creation_date
                });

            var prev = page > 0 ? Url.Link(nameof(GetResultsOfBestMatch), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetResultsOfBestMatch), new { page = page + 1, pageSize }) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = data
            };

            return Ok(result);


        }

        [HttpGet("{id}", Name = nameof(GetPostBestMatch))]
        public IActionResult GetPostBestMatch(int id)
        {
            var post = _dataService.ReturnQuestionById(id);

            //add visited 
            _dataService.AddVisitedPost(id);

            var result = new
            {
                Link = Url.Link(nameof(GetPostBestMatch), new { post.id }),
                //post.title,
                post.score,
                post.body,
                post.creation_date,
                answers = Url.Link(nameof(GetAnswersBestMatch), new { post.id }),
                comments = Url.Link(nameof(GetCommentsBestMatch), new { post.id }),
                linkedPosts = Url.Link(nameof(GetlinkedPosts), new { post.id }),
                showTags = Url.Link(nameof(GetshowTags), new { post.id }),
                addMark = Url.Link(nameof(addMark), new { post.id }),
                removeMark = Url.Link(nameof(removeMark), new { post.id }),
                //commentsOfAnswers = Url.Link(nameof(GetcommentsOfAnswers), new { post.id }),

            };

            return Ok(result);

        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswersBestMatch))]
        public IActionResult GetAnswersBestMatch(int id)
        {
            var answers = _dataService.ReturnAnswersById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostBestMatch), new { x.id }),
                    Parent = Url.Link(nameof(GetPostBestMatch), new { id }),
                    body = x.body,
                    x.creation_date,
                    x.score
                });

            return Ok(answers);
        }



        [HttpGet("{id}/comments", Name = nameof(GetCommentsBestMatch))]
        public IActionResult GetCommentsBestMatch(int id)
        {

            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetCommentsBestMatch), new { x.id }),
                    Parent = Url.Link(nameof(GetCommentsBestMatch), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });

            return Ok(comments);
        }

        [HttpGet("{id}/linkedPosts", Name = nameof(GetlinkedPosts))]
        public IActionResult GetlinkedPosts(int id)
        {

            var linkedPosts = _dataService.ReturnLinkPosts(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetlinkedPosts), new { x.id }),
                    Parent = Url.Link(nameof(GetlinkedPosts), new { id }),
                    title = x.title
                });

            return Ok(linkedPosts);
        }

        [HttpGet("{id}/showTags", Name = nameof(GetshowTags))]
        public IActionResult GetshowTags(int id)
        {

            var linkedPosts = _dataService.ReturnPostTags(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetshowTags), new { x.id }),
                    Parent = Url.Link(nameof(GetshowTags), new { id }),
                    name = x.name
                });

            return Ok(linkedPosts);
        }

        [HttpGet("{id}/commentsOfAnswers", Name = nameof(GetcommentsOfAnswers))]
        public IActionResult GetcommentsOfAnswers(int id)
        {
            //get comments of the answers id 


            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetcommentsOfAnswers), new { x.id }),
                    Parent = Url.Link(nameof(GetcommentsOfAnswers), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });

            return Ok(comments);
        }


        // Helpers 


        [HttpPatch("{id}/updateAnnotation")]
        public IActionResult UpdateAnnotation(int id, [FromBody] JsonPatchDocument<MarksModel> doc)
        {

            // This is a example body of a patch: 
            // [{"op": "replace", "path": "/annotation", "value": "testing"}]
            var mark = _dataService.GetMark(id);
            // not a valid id
            if (mark == null) return NotFound();

            // map to the model
            var model = _mapper.Map<MarksModel>(mark);
            // apply changes
            doc.ApplyTo(model);

            // now we want to map in the other direction from - MarksModel to mark
            // Add the ReverseMap() to the mapper config in StartUp.CreateMapper()
            //_mapper.Map(model, mark);

            _dataService.UpdateAnnotation(model.posts_id, model.annotation);

            return Ok();


        }

        [HttpGet("{id}/addMark", Name = nameof(addMark))]
        public IActionResult addMark(int id)
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

        [HttpGet("{id}/removeMark", Name = nameof(removeMark))]
        public IActionResult removeMark(int id)
        {

            var MarkPost = _dataService.DeleteMarkPost(id);
            return Ok(MarkPost);


        }





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
