using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.dbContext;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SovaWebService.Controllers
{
    [Route("api/graph")]
    public class TermNetworkController : Controller
    {
        private readonly IDataService _dataService;

        public TermNetworkController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/graph/{searchText}
        [HttpGet("{searchText}", Name = nameof(GetTermNetworkResult))]
        public IActionResult GetTermNetworkResult(string searchText)
        {


            return Ok(JsonConvert.SerializeObject(_dataService.TermNetworkList(searchText)));


        }
    }
}
