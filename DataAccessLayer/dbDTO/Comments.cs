using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class comments
    {

        public int id { get; set; }
        public int posts_id { get; set; }
        public int users_id { get; set; }
        public int score { get; set; }
        public string text { get; set; }
        public DateTime creation_date { get; set; }
    }
}
