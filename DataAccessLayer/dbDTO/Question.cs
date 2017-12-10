using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace DataAccessLayer.dbDTO
{
   public class Question
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime creation_date { get; set; }
        //public DateTime closed_date { get; set; }
        public int users_id { get; set; }
        public int score { get; set; }
        //public int accepted_answer_id { get; set; }
        
    }
}
