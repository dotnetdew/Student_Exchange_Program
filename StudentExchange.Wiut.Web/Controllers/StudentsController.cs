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
    private readonly IRepository<ContactDetails> _contactDetailsRepository;
    private readonly IRepository<EducationalDetails> _educationalDetailsRepository;
    private readonly IRepository<DisabilityLearningSupport> _disabilityLearningSupportRepository;
    private readonly IRepository<Housing> _housingRepository;
    private readonly IRepository<Student> _studentdRepository;
    public StudentsController(
        UserManager<Student> userManager, 
        IRepository<PersonalDetails> personalDetailsRepository,
        IRepository<ContactDetails> contactDetailsRepository,
        IRepository<Student> studentdRepository,
        IRepository<EducationalDetails> educationalDetailsRepository,
        IRepository<DisabilityLearningSupport> disabilityLearningSupportRepository,
        IRepository<Housing> housingRepository)
    {
        _userManager = userManager;
        _personalDetailsRepository = personalDetailsRepository;
        _studentdRepository = studentdRepository;
        _contactDetailsRepository = contactDetailsRepository;
        _educationalDetailsRepository = educationalDetailsRepository;
        _disabilityLearningSupportRepository = disabilityLearningSupportRepository;
        _housingRepository = housingRepository;
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

                return RedirectToAction("SaveContactDetails", new { studentId = vm.StudentId });
            }
        }
        else
            return View();
        
    }

    [HttpGet]
    public IActionResult SaveContactDetails(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var contactVM = new CreateContactDetailsVM() { StudentId = studentId };
        return View(contactVM);
    }

    [HttpPost]
    public IActionResult SaveContactDetails(CreateContactDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            var contactDetails = new ContactDetails()
            {
                Country = vm.Country,
                MobilePhoneNumber = vm.MobilePhoneNumber,
                OtherPhoneNumber = vm.OtherPhoneNumber,
                NextOfKinTitle = vm.NextOfKinTitle,
                NextOfKinForeName = vm.NextOfKinForeName,
                NextOfKinSurName = vm.NextOfKinSurName,
                Relationship = vm.Relationship,
                NextOfKinCountry = vm.NextOfKinCountry,
                NextOfKinMobilePhone = vm.NextOfKinMobilePhone,
                NextOfKinOtherPhone = vm.NextOfKinOtherPhone,
                EmailAddress = vm.EmailAddress,
                StudentId = vm.StudentId
            };
            _contactDetailsRepository.Add(contactDetails);
            _contactDetailsRepository.Save();

            return View(vm);
        }
        else
            return View();
    }

    [HttpGet]
    public IActionResult SaveEducationalDetails(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var educationalVM = new CreateEducationalDetailsVM() { StudentId = studentId };
        return View(educationalVM);
    }

    [HttpPost]
    public IActionResult SaveEducationalDetails(CreateEducationalDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            var educationalDetails = new EducationalDetails()
            {
                ExchangePartnerInstitution = vm.ExchangePartnerInstitution,
                ExactNameOfDegreeProgramme = vm.ExactNameOfDegreeProgramme,
                TotalLengthOfDegreeProgrammeInYears = vm.TotalLengthOfDegreeProgrammeInYears,
                ExpectedMonthOfGraduation = vm.ExpectedMonthOfGraduation,
                ExpectedYearOfGraduation = vm.ExpectedYearOfGraduation,
                IsEnglishFirstLanguage = vm.IsEnglishFirstLanguage,
                StudentId = vm.StudentId
            };

            _educationalDetailsRepository.Add(educationalDetails);
            _educationalDetailsRepository.Save();

            return View(vm);
        }
        else
            return View();
    }

    [HttpGet]
    public IActionResult SaveDisabilityLearningSupport(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var disablityLearningVM = new CreateDisabilityLearningSupportVM() { StudentId = studentId };
        return View(disablityLearningVM);
    }

    [HttpPost]
    public IActionResult SaveDisabilityLearningSupport(CreateDisabilityLearningSupportVM vm)
    {
        if (ModelState.IsValid)
        {
            var disabilityDetails = new DisabilityLearningSupport()
            {
                HaveADisablity = vm.HaveADisablity,
                StudentId = vm.StudentId
            };

            _disabilityLearningSupportRepository.Add(disabilityDetails);
            _disabilityLearningSupportRepository.Save();

            return View(vm);
        }
        else
            return View();
    }

    [HttpGet]
    public IActionResult SaveHousingDetails(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var housingVM = new CreateHousingVM() { StudentId = studentId };
        return View(housingVM);
    }

    [HttpPost]
    public IActionResult SaveHousingDetails(CreateHousingVM vm)
    {
        if (ModelState.IsValid)
        {
            var housing = new Housing()
            {
                WishToApplyForHousingInUniversity = vm.WishToApplyForHousingInUniversity,
                StudentId = vm.StudentId
            };

            _housingRepository.Add(housing);
            _housingRepository.Save();

            return View(vm);
        }
        else
            return View();
    }

    [HttpGet]
    public IActionResult SaveSubmission(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        //var housingVM = new CreateHousingVM() { StudentId = studentId };
        return View();
    }
}
