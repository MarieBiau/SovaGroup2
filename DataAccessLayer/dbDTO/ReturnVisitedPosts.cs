using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class ReturnVisitedPosts
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime view_date { get; set; }
    }
}
