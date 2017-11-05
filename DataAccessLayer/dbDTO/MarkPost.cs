using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.dbDTO
{
    public class MarkPost
    {
        public int id { get; set; }
        public int posts_id { get; set; }
        public int marking_type_id { get; set; }
        public DateTime creation_date { get; set; }
    }
}
