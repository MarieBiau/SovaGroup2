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
            var post = service.FindQuestionByString("java");
            Assert.Equal(9662, post.Count);


        }

        [Fact]
        public void ReturnSearches()
        {
            var service = new DataService();
            var searches = service.ReturnSearches();
            Assert.Equal(27, searches.Count);

        }
}

