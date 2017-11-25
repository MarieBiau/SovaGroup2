using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
   public class BestmatchKeywordList
    {
        public int id { get; set; }
        public string lemma { get; set; }
        public double weight { get; set; }
    }
}
