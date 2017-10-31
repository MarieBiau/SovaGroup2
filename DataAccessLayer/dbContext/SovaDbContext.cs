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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "server=localhost;database=mydb;uid=root;pwd=asas");
            
        }

 

    }
}
