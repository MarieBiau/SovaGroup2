using Xunit;
using DataAccessLayer.dbContext;

public class dataServiceTest
    {


        [Fact]
        public void GetUser_ValidId_ReturnsUserObject()
        {
            var service = new DataService();
        var user = service.GetUser(1);
        Assert.Equal("Jeff Atwood", user.DisplayName);
        //Assert.True(true);
        }

        [Fact]
        public void FindPostsbyString()
        {
            var service = new DataService();
            var post = service.FindQuestionByString("value of n");
            Assert.Equal(5, post.Count);


        }

        [Fact]
        public void ReturnSearches()
        {
            var service = new DataService();
            var searches = service.ReturnSearches();
            Assert.Equal(3, searches.Count);

        }

        [Fact]
        public void MarkPost()
        {
            var service = new DataService();
            var mark = service.MarkPost(9181, 2);
            Assert.Equal(true, mark);

    }

    [Fact]
        public void ReturnGoodMarks()
        {
            var service = new DataService();
            var ReturnGoodMarks = service.ReturnGoodMarks();
            Assert.Equal(2, ReturnGoodMarks.Count);

        }


        [Fact]
        public void UpdateAnnotation()
        {
            var service = new DataService();

            var result = service.UpdateAnnotation(22, "UpdatedName");
            Assert.True(result);

        }
        [Fact]
        public void ReturnVisitedPosts()
        {
            var service = new DataService();

            var result = service.ReturnVisitedPosts();
            Assert.Equal(1, result.Count);

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
}

