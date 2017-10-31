using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;

namespace SovaWebService.Controllers
{

    [Route("api/questions")]
    public class QuestionsController : Controller
    {

        private IDataService _dataService;

        public QuestionsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/Questions/java
        [HttpGet("{searchText}")]
        public IActionResult Get(string searchText)
        {
            if (_dataService.FindQuestionByString(searchText).Count > 0)
            {

                return Ok(_dataService.FindQuestionByString(searchText).Take(5));

            }
            else
            {
                return NotFound();
            }
        }
        
     
    }
}
