using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.dbDTO
{
    public class Users
    {
        public int Id { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        public string Location { get; set; }

        public int Age { get; set; }
    }
}
