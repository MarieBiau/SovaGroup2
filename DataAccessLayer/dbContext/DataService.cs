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


        public Posts FindPost(int id)
        {

            var post = db.Posts.FirstOrDefault(x => x.id == id);

            return post;
        }

        public List<Posts> FindAllPosts(int page, int pageSize)
        {
            List<Posts> listPosts = new List<Posts>();

            var posts = db.Posts;

            foreach (var post in posts)
            {

                listPosts.Add(post);

            }

            return listPosts
                .OrderBy(x => x.id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        public List<QuestionSearchResults> FindQuestionByString(string text, int page, int pageSize)
        {
            List<QuestionSearchResults> listQuestions = new List<QuestionSearchResults>();

                var Questions = db.QuestionSearchResults.FromSql("call search({0})", text);

                foreach (var Question in Questions)
                {

                    listQuestions.Add(Question);
                
                }
                
            return listQuestions
                .OrderBy(x => x.id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        public Posts ReturnQuestionById(int id)
        {
            var results = db.Posts.FirstOrDefault(x=>x.id == id);

            return results;
            //var results = db.Questions.FromSql("call return_question({0})", id);

            //if (results.Any())
            //{

            //    return results.FirstOrDefault(x => x.id == id);
            //}
            //else
            //{
            //    var returnAnswer = db.Questions.FromSql("call return_answer({0})", id);
            //    return returnAnswer.FirstOrDefault(x => x.id == id);

            //}

            //return results.FirstOrDefault(x => x.id == id);


        }

        public List<Comments> ReturnCommentsById(int id)
        {

            List<Comments> CommentList = new List<Comments>();
            var Comments =  db.Comments.Where(x => x.posts_id == id);

            foreach (var comment in Comments)
            {
                CommentList.Add(comment);
            }
            return  CommentList;
        }

        public List<AnswersSearchResults> ReturnAnswersById(int id)
        {

            List<AnswersSearchResults> answerList = new List<AnswersSearchResults>();
            var answers = db.AnswersSearchResults.FromSql("call return_answers({0})", id);

            foreach (var answer in answers)
            {
                answerList.Add(answer);
            }

            return answerList;
        }

        public int GetNumberOfResults(string text)
        {
            List<QuestionSearchResults> listQuestions = new List<QuestionSearchResults>();

            var Questions = db.QuestionSearchResults.FromSql("call search({0})", text);

            foreach (var Question in Questions)
            {

                listQuestions.Add(Question);

            }

            return listQuestions.Count();
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


        public bool  MarkPost(int post_id, int type)
        {

            var result = db.MarkPosts.FromSql("call mark_post({0},{1})", post_id, type);
            
            return result.Any();

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

        public Boolean UpdateAnnotation(int id, string annotation)
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


        public Marks GetMark(int id)
        {

            var mark = db.Marks.FirstOrDefault(x => x.posts_id == id);
            return mark;

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

        public List<BestMatch> BestMatches(string text)
        {
            List<BestMatch> listreturnPosts = new List<BestMatch>();

            var returnPosts = db.BestMatches.FromSql("call bestmatch({0})", text);
            foreach (var post in returnPosts)
            {
                listreturnPosts.Add(post);
            }
            return listreturnPosts;
        }

        public List<BestmatchKeywordList> BestmatchKeywordLists(string text)
        {
            List<BestmatchKeywordList> listreturnPosts = new List<BestmatchKeywordList>();
            var returnPosts = db.BestmatchKeywordLists.FromSql("call bestmatch_keyword_list({0})", text);
            foreach (var post in returnPosts)
            {
                listreturnPosts.Add(post);
            }
            return listreturnPosts;
        }

        public List<ClosestTerm> ClosestTerms(string text)
        {
            List<ClosestTerm> listClosestTerm = new List<ClosestTerm>();
            var returnclosest_terms = db.ClosestTerms.FromSql("call closest_terms({0})", text);
            foreach (var term in returnclosest_terms)
            {
                listClosestTerm.Add(term);
            }
            return listClosestTerm;

        }
        


    }
}
