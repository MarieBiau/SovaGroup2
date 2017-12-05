using System;

namespace DataAccessLayer.dbDTO
{
    public class posts
    {
        public int id { get; set; }
        public int post_type_id { get; set; }
        public DateTime creation_date { get; set; }
        public int score { get; set; }
        public string body { get; set; }
        public int user_id { get; set; }
    }
}
