using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovaWebService.Models
{
    public class SearchResultsModel
    {
        public string Url { get; set; }
        //maybe remove
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int score { get; set; }
        public string comment_text { get; set; }
        public string tag_name { get; set; }
    }
}
