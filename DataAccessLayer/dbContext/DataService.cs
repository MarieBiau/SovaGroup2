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

        public posts FindPost(int id)
        {
            using (var db = new SovaDbContext())
            {
                var post = db.posts.FirstOrDefault(x => x.id == id);
                return post;
            }
        }

        public List<posts> FindAllPosts(int page, int pageSize)
        {
            using (var db = new SovaDbContext())
            {
                List<posts> listPosts = new List<posts>();

                var posts = db.posts;

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
        
        public posts ReturnQuestionById(int id)
        {
            posts results;
            using (var db = new SovaDbContext())
            {
                results = db.posts.FirstOrDefault(x => x.id == id);
            }
            return results;
        }

        public bool AddVisitedPost(int id)
        {
            using (var db = new SovaDbContext())
            {

                var results = db.AddVisitedPost.FromSql("call add_visited_post({0})", id);
                return results.Any();
            }

        }

        public List<comments> ReturnCommentsById(int id)
        {
            using (var db = new SovaDbContext())
            {
                List<comments> CommentList = new List<comments>();
                var comments =  db.comments.Where(x => x.posts_id == id);

                foreach (var comment in comments)
                {
                    CommentList.Add(comment);
                }

                db.QuestionSearchResults.FromSql("call add_visited_post({0})", id);

                return CommentList;
            }
        }

        public List<AnswersSearchResults> ReturnAnswersById(int id)
        {
            using (var db = new SovaDbContext())
            {
                List<AnswersSearchResults> answerList = new List<AnswersSearchResults>();
                var answers = db.AnswersSearchResults.FromSql("call return_answers({0})", id);

                foreach (var answer in answers)
                {
                    answerList.Add(answer);
                }

                db.QuestionSearchResults.FromSql("call add_visited_post({0})", id);

                return answerList;
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

        public List<MarkPost>  MarkPost(int post_id, int type)
        {
            //using (var db = new SovaDbContext())
            //{
    
            //}

            ////1 Good
            ////2 Bad
            //MarkPost results = db.MarkPosts.FromSql("call mark_post({0},{1})", post_id, type);
            //return results;


            using (var db = new SovaDbContext())
            {
                List<MarkPost> listMarkPost = new List<MarkPost>();

                var marks = db.MarkPosts.FromSql("call mark_post({0},{1})", post_id, type);
                foreach (var mark in marks)
                {
                    listMarkPost.Add(mark);
                }

                return listMarkPost;
            }
        }

        public bool DeleteMarkPost(int post_id)
        {
            using (var db = new SovaDbContext())
            {
                var mark = db.marks.FirstOrDefault(x => x.posts_id == post_id);

                if (mark != null)
                {
                    db.marks.Remove(mark);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public List<ReturnGoodMarks> ReturnGoodMarks()
        {
            using (var db = new SovaDbContext())
            {
                List<ReturnGoodMarks> listReturnGoodMarks = new List<ReturnGoodMarks>();

                var marks = db.ReturnGoodMarks.FromSql("call return_good_marks()");
                foreach (var mark in marks)
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
                var marks = db.marks.FirstOrDefault(x => x.posts_id == id);

                if (marks != null)
                {
                    marks.annotation = annotation;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }


        public marks GetMark(int id)
        {
            using (var db = new SovaDbContext())
            {
                var mark = db.marks.FirstOrDefault(x => x.posts_id == id);
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

                var newestPosts = db.ReturnNewestQuestions.FromSql("call return_newest_questions({0})",amount);
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

        public List<BestMatch> BestMatches(string text, int page, int pageSize)
        {
            //update Searches table 
            AddSearches(text);
            using (var db = new SovaDbContext())
            {
                List<BestMatch> listreturnPosts = new List<BestMatch>();

                var returnPosts = db.BestMatches.FromSql("call bestmatch({0})", text);
                foreach (var post in returnPosts)
                {
                    listreturnPosts.Add(post);
                }
                return listreturnPosts
                    .OrderBy(x => x.id)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public int AddSearches(string text)
        {
            using (var db = new SovaDbContext())
            {
                var returnPosts = db.AddSearches.FromSql("call search({0})", text);
                return returnPosts.Count();
            }
        }

        public int BestMatchesTotal(string text)
        {
            using (var db = new SovaDbContext())
            {
                var returnPosts = db.BestMatches.FromSql("call bestmatch({0})", text);
                return returnPosts.Count();
            }
        }


        public List<BestmatchKeywordList> BestmatchKeywordLists(string text)
        {
            using (var db = new SovaDbContext())
            {
                List<BestmatchKeywordList> listreturnPosts = new List<BestmatchKeywordList>();
                var returnPosts = db.BestmatchKeywordLists.FromSql("call bestmatch_keyword_list({0})", text);
                foreach (var post in returnPosts)
                {
                    listreturnPosts.Add(post);
                }
                return listreturnPosts;
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

        public string TermNetworkList(string text)
        {
            using (var db = new SovaDbContext())
            {
                var termNetworks = db.TermNetworks.FromSql("call term_network({0})", text).ToList();

                return string.Join("", termNetworks.Select(x => x.graph)).Replace(",]", "]");
            }
        }

    }
}
