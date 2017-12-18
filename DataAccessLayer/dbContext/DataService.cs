using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.dbDTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace DataAccessLayer.dbContext
{
    public class DataService : IDataService
    {


        public Posts FindPost(int id)
        {
            using (var db = new SovaDbContext())
            {

                var post = db.Posts.FirstOrDefault(x => x.id == id);

                return post;
            }
        }

        public List<Posts> FindAllPosts(int page, int pageSize)
        {
            using (var db = new SovaDbContext())
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
        }
        
        public List<QuestionSearchResults> FindQuestionByString(string text, int page, int pageSize)
        {
            using (var db = new SovaDbContext())
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
        }
        
        public Question ReturnQuestionById(int id)
        {
            using (var db = new SovaDbContext())
            {


                var results = db.Questions.FromSql("call return_question({0})", id);
                //var returnQuestion = results.FirstOrDefault(x=>x.id == id);

                results.Any();
                return results.FirstOrDefault(x => x.id == id);
            }
        }

        public List<Comments> ReturnCommentsById(int id)
        {
            using (var db = new SovaDbContext())
            {


                //List<Question> answerList = new List<Question>();
                ////var AnsweriIds = db.Answers.Where(x => x.parent_id == id);
                //List<Question> listAnswerIds = new List<Question>();
                //var answerIds = db.Questions.FromSql("call return_answers_id({0})", id);

                List<Comments> CommentList = new List<Comments>();


                var Comments = db.Comments.Where(x => x.posts_id == id);


                foreach (var comment in Comments)
                {

                    CommentList.Add(comment);

                }

                return CommentList;
            }
        }
        
        public int GetNumberOfResults(string text)
        {
            using (var db = new SovaDbContext())
            {

                List<QuestionSearchResults> listQuestions = new List<QuestionSearchResults>();

                var Questions = db.QuestionSearchResults.FromSql("call search({0})", text);

                foreach (var Question in Questions)
                {

                    listQuestions.Add(Question);

                }

                return listQuestions.Count();
            }
        }

        public List<ReturnSearches> ReturnSearches()
        {
            using (var db = new SovaDbContext())
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


        public bool  MarkPost(int post_id, int type)
        {
            using (var db = new SovaDbContext())
            {

                var result = db.MarkPosts.FromSql("call mark_post({0},{1})", post_id, type);

                return result.Any();
            }
        }

        public List<ReturnGoodMarks> ReturnGoodMarks()
        {
            using (var db = new SovaDbContext())
            {

                List<ReturnGoodMarks> listReturnGoodMarks = new List<ReturnGoodMarks>();

                var Marks = db.ReturnGoodMarks.FromSql("call return_good_marks()");
                foreach (var mark in Marks)
                {

                    listReturnGoodMarks.Add(mark);

                }

                return listReturnGoodMarks;
            }
        }

        public Boolean UpdateAnnotation(int id, string annotation)
        {
            using (var db = new SovaDbContext())
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
        }


        public Marks GetMark(int id)
        {
            using (var db = new SovaDbContext())
            {


                var mark = db.Marks.FirstOrDefault(x => x.posts_id == id);
                return mark;
            }
        }


        public List<ReturnVisitedPosts> ReturnVisitedPosts()
        {
            using (var db = new SovaDbContext())
            {

                List<ReturnVisitedPosts> listReturnVisitedPosts = new List<ReturnVisitedPosts>();

                var visitedPosts = db.ReturnVisitedPosts.FromSql("call return_visited_posts()");
                foreach (var post in visitedPosts)
                {

                    listReturnVisitedPosts.Add(post);

                }

                return listReturnVisitedPosts;
            }
        }

        public List<ReturnNewestQuestions> ReturnNewestPosts(int amount)
        {
            using (var db = new SovaDbContext())
            {

                List<ReturnNewestQuestions> listReturnNewestQuestions = new List<ReturnNewestQuestions>();

                var newestPosts = db.ReturnNewestQuestions.FromSql("call return_newest_questions({0})", amount);
                foreach (var post in newestPosts)
                {

                    listReturnNewestQuestions.Add(post);

                }

                return listReturnNewestQuestions;
            }
        }


        public List<ReturnLinkPosts> ReturnLinkPosts(int id)
        {
            using (var db = new SovaDbContext())
            {

                List<ReturnLinkPosts> listReturnLinkPosts = new List<ReturnLinkPosts>();

                var linkPosts = db.ReturnLinkPosts.FromSql("call return_linkposts({0})", id);
                foreach (var post in linkPosts)
                {

                    listReturnLinkPosts.Add(post);

                }

                return listReturnLinkPosts;
            }
        }

        public List<ReturnPostTags> ReturnPostTags(int id)
        {
            using (var db = new SovaDbContext())
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

        //getting most used tags
        public List<PopularTags> PopularTagsList(int number)
        {
            using (var db = new SovaDbContext())
            {

                List<PopularTags> listreturnTags = new List<PopularTags>();
                var returnTags = db.PopularTags.FromSql("call count_tags_occurrences({0})", number);
                foreach (var tag in returnTags)
                {
                    listreturnTags.Add(tag);
                }
                return listreturnTags;
            }
        }
        

        public List<BestMatch> BestMatches(string text)
        {
            using (var db = new SovaDbContext())
            {

                List<BestMatch> listreturnPosts = new List<BestMatch>();

                var returnPosts = db.BestMatches.FromSql("call bestmatch({0})", text);
                foreach (var post in returnPosts)
                {
                    listreturnPosts.Add(post);
                }
                return listreturnPosts;
            }
        }

        public List<BestmatchKeywordList> BestmatchKeywordLists(string text)
        {
            using (var db = new SovaDbContext())
            {
                var returnPosts = db.BestmatchKeywordLists.FromSql("call bestmatch_keyword_list({0})", text).ToList();
                return returnPosts;
            }
        }

        public List<ClosestTerm> ClosestTerms(string text)
        {
            using (var db = new SovaDbContext())
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

        public string TermNetworkList(string text)
        {
            using (var db = new SovaDbContext())
            {
                var termNetworks = db.TermNetworks.FromSql("call term_network({0})", text).ToList();
                //if (termNetworks.ToString().Contains("graph: "))
                //{
                //    return ;
                //}
                //termNetworks.ToString().Replace("graph", " ");

                return string.Join("", termNetworks.Select(x => x.graph)).Replace(",]", "]");
            }
        }


    }
}
