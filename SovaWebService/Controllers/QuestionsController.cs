﻿using System;
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

        // GET: api/Questions
        [HttpGet(Name = nameof(GetPosts))]
        public IActionResult GetPosts( int page = 0, int pageSize = 10)
        {
            CheckPageSize(ref pageSize);

            var total = _dataService.FindAllPosts(page, pageSize).Count;
            var pages = GetTotalPages(pageSize, total);

            var posts = _dataService.FindAllPosts(page, pageSize)
                .Select(x => new {
                    Link = Url.Link(nameof(GetPosts), new { x.id }),
                    x.body
                });


            var data = _dataService.FindAllPosts(page, pageSize)
                .Select(x => new 
                {
                    Link = Url.Link(nameof(GetPosts), new { x.id }),
                    x.body,
                });

            var result = new
            {
                total,
                pages,
                prev = Link(nameof(GetPosts), page, pageSize, -1, () => page > 0),
                next = Link(nameof(GetPosts), page, pageSize, 1, () => page < total - 1),
                items = data
            };

            return Ok(result);


        }

        // GET: api/Questions/java
        [HttpGet("{searchText}", Name = nameof(GetResults))]
        public IActionResult GetResults(string searchText, int page = 0, int pageSize = 2)
        {

            CheckPageSize(ref pageSize);

            var total = _dataService.GetNumberOfResults(searchText);
            var totalPages = GetTotalPages(pageSize, total);

            var data = _dataService.FindQuestionByString(searchText, page , pageSize)
                .Select(x => new SearchResultsModel
                {
                    Url = Url.Link(nameof(GetResults), new { id = x.id }),
                    title = x.title,
                    body = x.body,
                    comment_text = x.comment_text,
                    score = x.score,
                    tag_name = x.tag_name
                });


            var result = new
            {
                Total_Results = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetResults), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetResults), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetResults), page, pageSize),
                Data = data
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
