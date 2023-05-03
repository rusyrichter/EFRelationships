using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRelationships.Data
{
    public class QuestionAnswerRepository
    {
        public string _connectionString { get; set; }
        public QuestionAnswerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private Tag GetTag(string name)
        {
            using var ctx = new QuestionAnswerDBContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }
        private int AddTag(string name)
        {
            using var ctx = new QuestionAnswerDBContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }
        public void AddQuestion(Question question, List<string> tags)
        {         
            using var context = new QuestionAnswerDBContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();

            int tagId;
            foreach (string tag in tags)
            {
               
                Tag t = GetTag(tag);
               
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }              
                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }
            context.SaveChanges();
        }
        public List<Question> GetQuestions()
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Questions.Include(q => q.Answers)
                                    .Include(q => q.QuestionsTags)
                                    .ThenInclude(qt => qt.Tag).ToList();     
        }
        public Question GetQuestionPerId(int id)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Questions.Include(q=> q.Answers)
                                    .ThenInclude(a=> a.User)
                                    .Include(q=> q.User)
                                    .Include(q => q.QuestionsTags)
                                    .ThenInclude(qt => qt.Tag)                                  
                                    .ToList().FirstOrDefault(q => q.Id == id);
        }
        public int GetUserIdPerEmail(string email)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Users.FirstOrDefault(q => q.Email == email ).Id;

        }
        public void AddAnswer(Answer answer)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
                     
            context.SaveChanges();
        }
    }
    }

