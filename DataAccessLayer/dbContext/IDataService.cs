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

        Question ReturnQuestionById(int id);


        List<Comments> ReturnCommentsById(int id);


        int GetNumberOfResults(string text);
        List<ReturnSearches> ReturnSearches();
        Boolean MarkPost(int post_id, int type);
        List<ReturnGoodMarks> ReturnGoodMarks();
        Boolean UpdateAnnotation(int id, string annotation);
        Marks GetMark(int id);

        List<PopularTags> PopularTagsList(int number);

        List<BestMatch> BestMatches(string text);
        List<BestmatchKeywordList> BestmatchKeywordLists(string text);
        List<ClosestTerm> ClosestTerms(string text);
        string TermNetworkList(string text);

    }
}
