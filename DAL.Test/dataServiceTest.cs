using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using DAL.dbContext;

namespace DAL.Test
{
    public class dataServiceTest
    {
        [Fact]
        public void FindPostsbyString()
        {
            var service = new DataService();
            var post = service.FindPost(19);


            Assert.Equal(1, post.id);


        }

        [Fact]
        public void GetUser_ValidId_ReturnsUserObject()
        {
            var service = new DataService();
            var user = service.GetUser(1);
            Assert.Equal("Jeff Atwood", user.DisplayName);
        }
    }
}
