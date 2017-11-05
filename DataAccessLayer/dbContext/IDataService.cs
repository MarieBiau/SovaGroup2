using System;
using System.Collections.Generic;
using DataAccessLayer.dbDTO;

namespace DataAccessLayer.dbContext
{
    public interface IDataService
    {

        Post FindPost(int id);
        List<QuestionSearchResults> FindQuestionByString(string searchText);
        List<ReturnSearches> ReturnSearches();
        Boolean MarkPost(int post_id, int type);
        List<ReturnGoodMarks> ReturnGoodMarks();
        Boolean UpdateAnnotation(int id, string annotation);

    }
}
