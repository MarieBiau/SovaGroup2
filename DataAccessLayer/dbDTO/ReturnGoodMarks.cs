using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class ReturnGoodMarks
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string annotation { get; set; }
        public DateTime creation_date { get; set; }

    }
}
