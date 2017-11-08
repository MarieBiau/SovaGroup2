using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovaWebService.Models
{
    public class MarksModel
    {
        public int id { get; set; }
        public int posts_id { get; set; }
        public int marking_type_id { get; set; }
        public DateTime creation_date { get; set; }
        public string annotation { get; set; }
    }
}
