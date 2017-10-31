using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.dbDTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.dbContext
{
    public class DataService : IDataService
    {
        SovaDbContext db = new SovaDbContext();

        public Post FindPost(int id)
        {



            SqlParameter prm1 = new SqlParameter("@search_text", "java");
            var posts =  db.Database.ExecuteSqlCommand("search @search_text", prm1);

           

            var post = db.Posts.FirstOrDefault(x => x.id == id);
            //if (post != null)
            //{
            //    return post;
            //}
            //return null;
            return posts;
        }

        //Already in the database functions
        public Users GetUser(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }

    }
}
