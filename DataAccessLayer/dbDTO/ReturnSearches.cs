using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLayer.dbDTO
{
   public class ReturnSearches
    {
        public int id { get; set; }
        public string text { get; set; }
        public DateTime search_date { get; set; }
    }
}
