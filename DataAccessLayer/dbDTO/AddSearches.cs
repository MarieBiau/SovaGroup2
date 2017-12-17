using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
    public class AddSearches
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int score { get; set; }
        public string comment_text { get; set; }
        public string tag_name { get; set; }

    }
}
