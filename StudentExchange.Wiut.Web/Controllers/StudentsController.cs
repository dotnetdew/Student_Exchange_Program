using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories;
using StudentExchange.Wiut.Web.ViewModels;
using System.Security.Claims;

namespace StudentExchange.Wiut.Web.Controllers;

[Authorize]
public class StudentsController : Controller
{
    private readonly UserManager<Student> _userManager;
    private readonly IRepository<PersonalDetails> _personalDetailsRepository;
    private readonly IRepository<Student> _studentdRepository;
    public StudentsController(
        UserManager<Student> userManager, 
        IRepository<PersonalDetails> personalDetailsRepository, 
        IRepository<Student> studentdRepository)
    {
        _userManager = userManager;
        _personalDetailsRepository = personalDetailsRepository;
        _studentdRepository = studentdRepository;
    }

    public IActionResult MyApplications()
    {
        return View();
    }

    public async Task<IActionResult> ApplicationChecklist()
    {
        var user_id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(user_id);

        return View(user);
    }

    [HttpGet]
    public IActionResult GetCountries()
    {
        using (var client =  new HttpClient())
        {
            var response = client.GetAsync("https://country.io/names.json").Result;
            response.EnsureSuccessStatusCode();
            var data = response.Content.ReadAsStringAsync().Result;
            return Json(data);
        }
    }

    [HttpGet]
    public IActionResult SavePersonalDetails(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var personalVM = new CreatePersonalDetailsVM() { StudentId = studentId };
        return View(personalVM);
    }

    [HttpPost]
    public IActionResult SavePersonalDetails(CreatePersonalDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            using (var stream = new MemoryStream())
            {
                vm.PassportFile.CopyTo(stream);
                var personalDetails = new PersonalDetails()
                {
                    Title = vm.Title,
                    ForeName = vm.ForeName,
                    SecondForeName = vm.SecondForeName,
                    ThirdForeName = vm.ThirdForeName,
                    FamilyName = vm.FamilyName,
                    PrefferedName = vm.PrefferedName,
                    PreviousFamilyName = vm.PreviousFamilyName,
                    DateOfBirth = vm.DateOfBirth,
                    Gender = vm.Gender,
                    UniversityStudentId = vm.UniversityStudentId,
                    CountryOfBirth = vm.CountryOfBirth,
                    Nationality = vm.Nationality,
                    PassportNumber = vm.PassportNumber,
                    DateOfIssue = vm.DateOfIssue,
                    DateOfExpiry = vm.DateOfExpiry,
                    PassportFile = stream.ToArray(),
                    PassportFileName = vm.PassportFile.FileName,
                    PassportFileType = vm.PassportFile.ContentType,
                    StudentId = vm.StudentId
                };
                _personalDetailsRepository.Add(personalDetails);
                _personalDetailsRepository.Save();
            }
        }
        return RedirectToAction("SaveContactDetails", new {studentId = vm.StudentId });
    }

    [HttpGet]
    public IActionResult SaveContactDetails(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var contactVM = new CreateContactDetailsVM() { StudentId = studentId };
        return View(contactVM);
    }

    //[HttpPost]
    //public IActionResult SaveContactDetails(CreateContactDetailsVM vm)
    //{
    //    if (ModelState.IsValid)
    //    {

    //    }
    //}
}
