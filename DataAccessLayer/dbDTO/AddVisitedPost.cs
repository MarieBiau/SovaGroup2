using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
    public class AddVisitedPost
    {
        public int id { get; set; }
        public int posts_id { get; set; }
        public DateTime view_date { get; set; }
    }
}
