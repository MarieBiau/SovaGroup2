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
                    body = x.body.Substring(0, x.body.Length < 300 ? x.body.Length : 300),
                    annotation = x.annotation,
                    date = x.creation_date
                });
            var result = new
            {
                items = goodMarksList
            };

            return Ok(result);
        }

        //update annotation {id}
        [HttpPatch("{id}")]
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
            
            Boolean updateAnnotation = _dataService.UpdateAnnotation(model.id,model.annotation);

            return NoContent();
            

        }

        //Create new mark Action controller




    }
}
