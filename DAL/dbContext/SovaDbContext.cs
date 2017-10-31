using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.dbDTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.dbContext
{
    class SovaDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Users> Users { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "server=localhost;database=mydb;uid=root;pwd=asas");
            
        }

 

    }
}
