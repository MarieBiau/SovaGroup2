using DataAccessLayer.dbContext;
using Xunit;

namespace DataAccessLayer.Test
{
    public class DataServiceTest
    {

        [Fact]
        public void FindPostById()
        {
            var service = new DataService();
            var post = service.FindPost(71);

            Assert.Equal(71, post.id);

        }

        [Fact]
        public void GetAllposts()
        {
            var service = new DataService();
            var posts = service.FindAllPosts(0,10);

            Assert.Equal(10, posts.Count);

        }

        [Fact]
        public void FindPostsbyString()
        {
            var service = new DataService();
            var post = service.FindQuestionByString("java" , 5, 5);
            Assert.Equal(5, post.Count);


        }

        [Fact]
        public void ReturnSearches()
        {
            var service = new DataService();
            var searches = service.ReturnSearches();
            Assert.Equal(0, searches.Count);

        }

        [Fact]
        public void MarkPost()
        {
            var service = new DataService();
            var mark = service.MarkPost(1243, 1);

            Assert.Equal(true, mark);

        }

        [Fact]
        public void ReturnGoodMarks()
        {
            var service = new DataService();
            var ReturnGoodMarks = service.ReturnGoodMarks();
            Assert.Equal(0, ReturnGoodMarks.Count);

        }


        [Fact]
        public void UpdateAnnotation()
        {
            var service = new DataService();

            var result = service.UpdateAnnotation(19, "UpdatedName");
            Assert.False(result);

        }
        [Fact]
        public void ReturnVisitedPosts()
        {
            var service = new DataService();

            var result = service.ReturnVisitedPosts();
            Assert.Equal(0, result.Count);

        }
        
        [Fact]
        public void ReturnNewestPosts()
        {
            var service = new DataService();

            var result = service.ReturnNewestPosts(10);
            Assert.Equal(10, result.Count);
        
        }

        [Fact]
        public void ReturnLinkPosts()
        {
            var service = new DataService();

            var result = service.ReturnLinkPosts(19);
            Assert.Equal(2, result.Count);

        }
        [Fact]
        public void ReturnPostTags()
        {
            var service = new DataService();

            var result = service.ReturnPostTags(19);
            Assert.Equal(5, result.Count);

        }

        [Fact]
        public void ReturnBestMatch()
        {
            var service = new DataService();
            var result = service.BestMatches("java",0,0);
            Assert.Equal(558, result.Count);

        }
        [Fact]
        public void ReturnBestMatchKeywordList()
        {
            var service = new DataService();
            var result = service.BestmatchKeywordLists("java");
            Assert.Equal(6131, result.Count);
        }
        [Fact]
        public void ReturnClosestTerms()
        {
            var service = new DataService();
            var result = service.ClosestTerms("java");
            Assert.Equal(12, result.Count);

        }
        [Fact]
        public void ReturnAnswersById()
        {
            var service = new DataService();
            var result = service.ReturnAnswersById(19);
            Assert.Equal(21, result.Count);

        }

        [Fact]
        public void DeleteMarkPostbyId()
        {
            var service = new DataService();
            var result = service.DeleteMarkPost(19);
            Assert.Equal(true, result);

        }

    }
}

