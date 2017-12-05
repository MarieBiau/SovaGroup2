using System;
using System.Collections.Generic;
using DataAccessLayer.dbDTO;

namespace DataAccessLayer.dbContext
{
    public interface IDataService
    {

        posts FindPost(int id);
        users GetUser(int userId);
        List<QuestionSearchResults> FindQuestionByString(string searchText, int page, int pageSize);
        int GetNumberOfResults(string text);
        List<ReturnSearches> ReturnSearches();
        Boolean MarkPost(int post_id, int type);
        List<ReturnGoodMarks> ReturnGoodMarks();
        Boolean UpdateAnnotation(int id, string annotation);
        marks GetMark(int id);
        List<ReturnVisitedPosts> ReturnVisitedPosts();
        List<ReturnNewestQuestions> ReturnNewestPosts(int amount);
        List<ReturnLinkPosts> ReturnLinkPosts(int id);
        List<ReturnPostTags> ReturnPostTags(int id);


        List<BestMatch> BestMatches(string text);
        List<BestmatchKeywordList> BestmatchKeywordLists(string text);
        List<ClosestTerm> ClosestTerms(string text);

    }
}
