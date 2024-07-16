using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.ViewModels.Student;

namespace StudentExchange.Wiut.Web.Areas.Admin.Controllers;

[Authorize(Policy = "AdminPolicy")]
[Area("Admin")]
public class ManageController : Controller
{
    private readonly UserManager<Student> _userManager;

    public ManageController(UserManager<Student> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var VMs = users.Select(u => new StudentVM
        {
            Id = u.Id,
            Title = u.Title,
            ForeName = u.ForeName,
            FamilyName = u.FamilyName,
            Email = u.Email,
            DateOfBirth = u.DateOfBirth,
        });
        return View(VMs);
    }

    public async Task<IActionResult> Details(string studentId)
    {
        if (studentId == null)
        {
            return NotFound();
        }

        var student = await _userManager.FindByIdAsync(studentId);
        if (student == null)
        {
            return NotFound();
        }


        return View();
    }
}