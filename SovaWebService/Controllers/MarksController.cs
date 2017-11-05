using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using DataAccessLayer.dbDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{

    [Route("api/marks")]
    public class MarksController : Controller
    {

        private IDataService _dataService;

        public MarksController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/Marks
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Good marks</returns>
        [HttpGet]
        public IActionResult Get()
        {
            if (_dataService.ReturnGoodMarks().Count > 0)
            {

                return Ok(_dataService.ReturnGoodMarks());

            }
            else
            {
                return NotFound();
            }
        }

        //update annotation {id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody]ReturnGoodMarks content)
        {
            var categoryUpdate = _dataService.UpdateAnnotation(content.id, content.annotation);

            if (categoryUpdate == false) return NotFound();
            return Ok();

        }


    }
}
