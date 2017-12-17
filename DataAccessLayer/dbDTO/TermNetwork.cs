using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.dbDTO
{
    public class TermNetwork
    {
        [Key]
        public string graph { get; set; }

    }
}
