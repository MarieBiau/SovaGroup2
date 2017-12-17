using System;
using System.Data;
using DataAccessLayer.dbDTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.dbContext
{
    class SovaDbContext : DbContext
    {
        public DbSet<posts> posts { get; set; }
        public DbSet<QuestionSearchResults> QuestionSearchResults { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ReturnSearches> ReturnSearches { get; set; }
        public DbSet<MarkPost> MarkPosts { get; set; }
        public DbSet<ReturnGoodMarks> ReturnGoodMarks { get; set; }
        public DbSet<marks> marks { get; set; }
        public DbSet<ReturnVisitedPosts> ReturnVisitedPosts { get; set; }
        public DbSet<ReturnNewestQuestions> ReturnNewestQuestions { get; set; }
        public DbSet<ReturnLinkPosts> ReturnLinkPosts { get; set; }
        public DbSet<ReturnPostTags> ReturnPostTags { get; set; }
        public DbSet<BestMatch> BestMatches { get; set; }
        public DbSet<BestmatchKeywordList> BestmatchKeywordLists { get; set; }
        public DbSet<ClosestTerm> ClosestTerms { get; set; }
        public DbSet<answers> answers { get; set; }
        public DbSet<comments> comments { get; set; }
        public DbSet<AnswersSearchResults> AnswersSearchResults { get; set; }
        public DbSet<PopularTags> PopularTags { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "server=localhost;database=mydb;uid=root;pwd=myriam1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //do we need it because these tables are same within databas schema???
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.id).HasColumnName("id");
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.title).HasColumnName("title");
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.body).HasColumnName("body");
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.score).HasColumnName("score");
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.comment_text).HasColumnName("comment_text");
            modelBuilder.Entity<QuestionSearchResults>().Property(x => x.tag_name).HasColumnName("tag_name");
        }

    }
}
