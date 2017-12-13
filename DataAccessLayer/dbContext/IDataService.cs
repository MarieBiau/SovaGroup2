using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.dbDTO;

namespace DataAccessLayer.dbContext
{
    public interface IDataService
    {

        Posts FindPost(int id);
        List<Posts> FindAllPosts(int page, int pageSize);
        List<QuestionSearchResults> FindQuestionByString(string searchText, int page, int pageSize);

        Posts ReturnQuestionById(int id);


        List<Comments> ReturnCommentsById(int id); 
        List<AnswersSearchResults> ReturnAnswersById(int id);
        List<ReturnNewestQuestions> ReturnNewestPosts(int id);

        int GetNumberOfResults(string text);
        List<ReturnSearches> ReturnSearches();
        List<ReturnVisitedPosts> ReturnVisitedPosts();

        Boolean MarkPost(int post_id, int type);
        Boolean DeleteMarkPost(int id);


        List<ReturnGoodMarks> ReturnGoodMarks();
        Boolean UpdateAnnotation(int id, string annotation);
        Marks GetMark(int id);

        List<BestMatch> BestMatches(string text, int page, int pageSize);
        List<BestmatchKeywordList> BestmatchKeywordLists(string text);
        List<ClosestTerm> ClosestTerms(string text);

    }
}
