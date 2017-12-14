using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class answers
    {

        [Key]
        public int posts_id { get; set; }
        public int parent_id { get; set; }
        

    }
}
