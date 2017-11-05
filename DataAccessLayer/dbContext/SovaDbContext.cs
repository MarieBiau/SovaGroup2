using System;
using System.Data;
using DataAccessLayer.dbDTO;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.dbContext
{
    class SovaDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<QuestionSearchResults> Questions { get; set; }
        public DbSet<ReturnSearches> ReturnSearches { get; set; }
        public DbSet<MarkPost> MarkPosts { get; set; }
        public DbSet<ReturnGoodMarks> ReturnGoodMarks { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<ReturnVisitedPosts> ReturnVisitedPosts { get; set; }
        public DbSet<ReturnNewestQuestions> ReturnNewestQuestions { get; set; }
        public DbSet<ReturnLinkPosts> ReturnLinkPosts { get; set; }
        public DbSet<ReturnPostTags> ReturnPostTags { get; set; }

        






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "server=localhost;database=mydb;uid=root;pwd=asas");
            
        }

 

    }
}
