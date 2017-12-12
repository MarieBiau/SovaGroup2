using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.dbDTO
{
    public class PopularTags
    {
        [Key]
        public int tags_id { get; set; }
        public string name { get; set; }
        public int occurrences { get; set; }

    }
}
