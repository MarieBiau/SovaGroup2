﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.dbDTO
{
  public class BestMatch
    {
        public int id { get; set; }
        public double rank { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime creation_date { get; set; }
        
    }
}
