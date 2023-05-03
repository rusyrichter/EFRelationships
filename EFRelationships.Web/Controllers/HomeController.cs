using EFRelationships.Data;
using EFRelationships.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EFRelationships.Web.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var repo = new QuestionAnswerRepository(_connectionString);
            var vm = new QuestionViewModel
            {
                Questions = repo.GetQuestions()
            };
            return View(vm);
        }
       


    }
}