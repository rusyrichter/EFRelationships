using EFRelationships.Data;
using EFRelationships.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EFRelationships.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private string _connectionString;
        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            var currentUser = User.Identity.Name;
            var repo = new QuestionAnswerRepository(_connectionString);
            question.DatePosted = DateTime.Now;
            question.UserId = repo.GetUserIdPerEmail(currentUser);
           
           
            repo.AddQuestion(question, tags);
            return Redirect("/Home/Index");
        }
        public IActionResult ViewQuestion(int id)
        {          
            var repo = new QuestionAnswerRepository(_connectionString);

            var vm = new QuestionViewModel
            {
                Question = repo.GetQuestionPerId(id), 
            };
           
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddAnswer(int questionId, string text)
        {
            var currentUser = User.Identity.Name;
            var repo = new QuestionAnswerRepository(_connectionString);
            var answer = new Answer
            {
                Text = text,
                Date = DateTime.Now,
                QuestionId = questionId,
                UserId = repo.GetUserIdPerEmail(currentUser),
            };
            repo.AddAnswer(answer);
            return Redirect($"/Home/Index");
        }
    }
}
