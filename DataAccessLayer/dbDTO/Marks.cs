using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
  public  class marks
    {
        public int id { get; set; }
        public int posts_id { get; set; }
        public int marking_type_id { get; set; }
        public DateTime creation_date { get; set; }
        public string annotation { get; set; }
    }
}
