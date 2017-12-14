using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class AnswersSearchResults
    {

        public int id { get; set; }
        public string body { get; set; }
        public DateTime creation_date { get; set; }
        public int score { get; set; }
        public int users_id { get; set; }
    }
}
