using Microsoft.AspNetCore.Mvc;

namespace StudentExchange.Wiut.Web.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult MyApplications()
        {
            return View();
        }

        public IActionResult ApplicationChecklist()
        {
            return View();
        }
    }
}
