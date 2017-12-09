using System;

namespace DataAccessLayer.dbDTO
{
    public class Post
    {

        public int id { get; set; }
        public int post_type_id { get; set; }
        public DateTime creation_date { get; set; }
        public int score { get; set; }
        public string body { get; set; }
        public int users_id { get; set; }

    }
}
