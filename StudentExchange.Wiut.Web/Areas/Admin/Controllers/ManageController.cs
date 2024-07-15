using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentExchange.Wiut.Web.Areas.Admin.Controllers;

[Authorize(Policy = "AdminPolicy")]
[Area("Admin")]
public class ManageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
