using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.dbDTO;

namespace DataAccessLayer.dbContext
{
    public interface IDataService
    {

        posts FindPost(int id);
        List<posts> FindAllPosts(int page, int pageSize);
        List<QuestionSearchResults> FindQuestionByString(string searchText, int page, int pageSize);

        posts ReturnQuestionById(int id);


        List<comments> ReturnCommentsById(int id); 
        List<AnswersSearchResults> ReturnAnswersById(int id);
        List<ReturnNewestQuestions> ReturnNewestPosts(int id);

        int GetNumberOfResults(string text);
        List<ReturnSearches> ReturnSearches();
        List<ReturnVisitedPosts> ReturnVisitedPosts();

        List<MarkPost> MarkPost(int post_id, int type);
        Boolean DeleteMarkPost(int id);


        List<ReturnGoodMarks> ReturnGoodMarks();
        Boolean UpdateAnnotation(int id, string annotation);
        marks GetMark(int id);

        List<BestMatch> BestMatches(string text, int page, int pageSize);
        int BestMatchesTotal(string text);
        List<BestmatchKeywordList> BestmatchKeywordLists(string text);
        List<ClosestTerm> ClosestTerms(string text);

        List<ReturnLinkPosts> ReturnLinkPosts(int id);
        List<ReturnPostTags> ReturnPostTags(int id);

        List<PopularTags> PopularTagsList(int number);

        bool AddVisitedPost(int id);
        string TermNetworkList(string text);


    }
}
