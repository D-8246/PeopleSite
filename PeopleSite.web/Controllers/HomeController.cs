using Microsoft.AspNetCore.Mvc;
using PeopleSite.Data;
using PeopleSite.web.Models;
using System.Diagnostics;

namespace PeopleSite.web.Controllers
{
    public class HomeController : Controller
    {
        private static string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;TrustServerCertificate=yes;";
        PeopleManager manager = new(_connectionString);

        public IActionResult Index()
        {
            var ivm = new IndexViewModel
            {
                People = manager.GetAll(),
            };
            if (TempData["message"] != null)
            {
                ivm.Message = TempData["message"].ToString();
            }
            return View(ivm);
        }

        public IActionResult AddPeople()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPeople(List<Person> people)
        {
            int i = manager.AddPeople(people);
            if (i == 1)
            {
                TempData["message"] = "1 person added successfully";
            }
            else if (i > 1)
            {
                TempData["message"] = $"{i} people added successfully";
            }
            else
            {
                TempData["message"] = "Invalid input, try again";
            }
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult DeleteAll(List<int> ids)
        {
            manager.DeleteAll(ids);
            if (ids.Count == 1)
            {
                TempData["message"] = "1 person deleted successfully";
            }
            else if (ids.Count > 1)
            {
                TempData["message"] = $"{ids.Count} people deleted successfully";
            }
            return Redirect("/");
        }
    }
}
