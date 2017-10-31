using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.dbDTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.dbContext
{
    public class DataService : IDataService
    {
        SovaDbContext db = new SovaDbContext();

        public Post FindPost(int id)
        {


            var post = db.Posts.FirstOrDefault(x => x.id == id);
            //if (post != null)
            //{
            //    return post;
            //}
            //return null;
            return post;
        }

        //Already in the database functions
        public Users GetUser(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }


        /// <summary>
        /// Store procedure call to retrieve questions based on string entered
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<QuestionSearchResults> FindQuestionByString(string text)
        {
            List<QuestionSearchResults> listQuestions = new List<QuestionSearchResults>();

                var Questions = db.Questions.FromSql("call search({0})", "java");
                foreach (var Question in Questions)
                {

                    listQuestions.Add(Question);
                
                }
                
            return listQuestions;
        }

        public List<ReturnSearches> ReturnSearches()
        {
            List<ReturnSearches> listSearches = new List<ReturnSearches>();

            var Searches = db.ReturnSearches.FromSql("call return_searches()");
            foreach (var search in Searches)
            {

                listSearches.Add(search);

            }

            return listSearches;
        }

    }
}
