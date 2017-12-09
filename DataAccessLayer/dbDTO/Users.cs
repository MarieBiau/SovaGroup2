using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.dbDTO
{
    public class users
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
