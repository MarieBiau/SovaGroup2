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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Post FindPost(int id)
        {

            var post = db.Posts.FirstOrDefault(x => x.id == id);

            return post;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

                var Questions = db.Questions.FromSql("call search({0})", text);
                foreach (var Question in Questions)
                {

                    listQuestions.Add(Question);
                
                }
                
            return listQuestions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post_id">PostId</param>
        /// <param name="type">1=Question,2=Answer</param>
        /// <returns></returns>
        public bool MarkPost(int post_id, int type)
        {

            db.MarkPosts.FromSql("call mark_post({0},{1})", post_id, type);

       
            //if (results != null)
            //{

            //    return true;
            //}

            return true;

        }

        public List<ReturnGoodMarks> ReturnGoodMarks()
        {
            List<ReturnGoodMarks> listReturnGoodMarks = new List<ReturnGoodMarks>();

            var Marks = db.ReturnGoodMarks.FromSql("call return_good_marks()");
            foreach (var mark in Marks)
            {

                listReturnGoodMarks.Add(mark);

            }

            return listReturnGoodMarks;

        }

        public bool UpdateAnnotation(int id, string annotation)
        {

            var marks = db.Marks.FirstOrDefault(x => x.id == id);
            if (marks != null)
            {
                marks.annotation = annotation;

                db.SaveChanges();
                return true;
            }
            return false;

        }

        public List<ReturnVisitedPosts> ReturnVisitedPosts()
        {
            List<ReturnVisitedPosts> listReturnVisitedPosts = new List<ReturnVisitedPosts>();

            var visitedPosts = db.ReturnVisitedPosts.FromSql("call return_visited_posts()");
            foreach (var post in visitedPosts)
            {

                listReturnVisitedPosts.Add(post);

            }

            return listReturnVisitedPosts;

        }

        public List<ReturnNewestQuestions> ReturnNewestPosts(int amount)
        {
            List<ReturnNewestQuestions> listReturnNewestQuestions = new List<ReturnNewestQuestions>();

            var newestPosts = db.ReturnNewestQuestions.FromSql("call return_newest_questions({0})",amount);
            foreach (var post in newestPosts)
            {

                listReturnNewestQuestions.Add(post);

            }

            return listReturnNewestQuestions;

        }


        public List<ReturnLinkPosts> ReturnLinkPosts(int id)
        {
            List<ReturnLinkPosts> listReturnLinkPosts = new List<ReturnLinkPosts>();

            var linkPosts = db.ReturnLinkPosts.FromSql("call return_linkposts({0})", id);
            foreach (var post in linkPosts)
            {

                listReturnLinkPosts.Add(post);

            }

            return listReturnLinkPosts;

        }
        
        public List<ReturnPostTags> ReturnPostTags(int id)
        {
            List<ReturnPostTags> listReturnPostTags = new List<ReturnPostTags>();

            var postTags = db.ReturnPostTags.FromSql("call return_tags({0})", id);
            foreach (var tag in postTags)
            {

                listReturnPostTags.Add(tag);

            }

            return listReturnPostTags;

        }

    }
}
