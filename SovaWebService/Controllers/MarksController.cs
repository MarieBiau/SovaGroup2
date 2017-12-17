using System;
using System.Linq;
using AutoMapper;
using DataAccessLayer.dbContext;
using DataAccessLayer.dbDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SovaWebService.Models;

namespace SovaWebService.Controllers
{

    [Route("api/marks")]
    public class MarksController : Controller
    {

        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public MarksController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {

            var goodMarksList = _dataService.ReturnGoodMarks()
                .Select(x => new
                {
                    title = x.title,
                    body = x.body.Substring(3, 100),
                    annotation = x.annotation,
                    date = x.creation_date,
                    Link = Url.Link(nameof(GetPostMarks), new { x.id }),
                    id = x.id

                });

            var result = new
            {
                items = goodMarksList
            };

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetPostMarks))]
        public IActionResult GetPostMarks(int id)
        {
            var post = _dataService.ReturnQuestionById(id);

            //add visited 
            //_dataService.AddVisitedPost(id);

            var result = new
            {
                Link = Url.Link(nameof(GetPostMarks), new { post.id }),
                //post.title,
                post.score,
                post.body,
                answers = Url.Link(nameof(GetAnswersMarks), new { post.id }),
                comments = Url.Link(nameof(GetCommentsMarks), new { post.id }),
                linkedPosts = Url.Link(nameof(GetlinkedMarks), new { post.id }),
                showTags = Url.Link(nameof(GetshowTagsMarks), new { post.id }),
                
                //commentsOfAnswers = Url.Link(nameof(GetcommentsOfAnswers), new { post.id }),

            };

            return Ok(result);

        }

        [HttpGet("{id}/answers", Name = nameof(GetAnswersMarks))]
        public IActionResult GetAnswersMarks(int id)
        {
            var answers = _dataService.ReturnAnswersById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPostMarks), new { x.id }),
                    Parent = Url.Link(nameof(GetPostMarks), new { id }),
                    body = x.body,
                    x.creation_date,
                    x.score
                });

            return Ok(answers);
        }



        [HttpGet("{id}/comments", Name = nameof(GetCommentsMarks))]
        public IActionResult GetCommentsMarks(int id)
        {

            var comments = _dataService.ReturnCommentsById(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetCommentsMarks), new { x.id }),
                    Parent = Url.Link(nameof(GetCommentsMarks), new { id }),
                    body = x.text,
                    x.creation_date,
                    x.score
                });

            return Ok(comments);
        }

        [HttpGet("{id}/linkedPosts", Name = nameof(GetlinkedMarks))]
        public IActionResult GetlinkedMarks(int id)
        {

            var linkedPosts = _dataService.ReturnLinkPosts(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetlinkedMarks), new { x.id }),
                    Parent = Url.Link(nameof(GetlinkedMarks), new { id }),
                    title = x.title
                });

            return Ok(linkedPosts);
        }

        [HttpGet("{id}/showTags", Name = nameof(GetshowTagsMarks))]
        public IActionResult GetshowTagsMarks(int id)
        {

            var linkedPosts = _dataService.ReturnPostTags(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetshowTagsMarks), new { x.id }),
                    Parent = Url.Link(nameof(GetshowTagsMarks), new { id }),
                    name = x.name
                });

            return Ok(linkedPosts);
        }

        //[HttpGet("{id}/commentsOfAnswers", Name = nameof(GetcommentsOfAnswers))]
        //public IActionResult GetcommentsOfAnswers(int id)
        //{
        //    //get comments of the answers id 
        //    var answers = _dataService.ReturnAnswersById(id)
        //        .Select(x => new
        //        {
        //            Link = Url.Link(nameof(GetPostBestMatch), new { x.id }),
        //            Parent = Url.Link(nameof(GetPostBestMatch), new { id }),
        //            body = x.body,
        //            x.creation_date,
        //            x.score
        //        });


        //    var comments = _dataService.ReturnCommentsById(id)
        //        .Select(x => new
        //        {
        //            Link = Url.Link(nameof(GetCommentsBestMatch), new { x.id }),
        //            Parent = Url.Link(nameof(GetCommentsBestMatch), new { id }),
        //            body = x.text,
        //            x.creation_date,
        //            x.score
        //        });

        //    return Ok(comments);
        //}





        //update annotation {id}
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

        [HttpGet("{id}/addMark")]
        public IActionResult addMark(int id)
        {

            var MarkPost = _dataService.MarkPost(id, 1);

            return Ok(MarkPost);


        }

        [HttpGet("{id}/removeMark")]
        public IActionResult removeMark(int id)
        {

            var MarkPost = _dataService.DeleteMarkPost(id);
            return Ok(MarkPost);


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
