using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRelationships.Data
{

   
        public class QuestionAnswerDBContext : DbContext
        {
            private string _connectionString;

            public QuestionAnswerDBContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            public DbSet<Question> Questions { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<QuestionsTags> QuestionsTags { get; set; }
            public DbSet<Answer> Answers { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }


                modelBuilder.Entity<QuestionsTags>()
                    .HasKey(qt => new { qt.QuestionId, qt.TagId });

            }
        }
    }
