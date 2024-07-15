using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentExchange.Wiut.Web.Areas.Admin.Controllers;

[Authorize(Policy = "AdminPolicy")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
