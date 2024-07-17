using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;
using StudentExchange.Wiut.Web.ViewModels.Student;

namespace StudentExchange.Wiut.Web.Areas.Admin.Controllers;

[Authorize(Policy = "AdminPolicy")]
[Area("Admin")]
public class ManageController : Controller
{
    private readonly UserManager<Student> _userManager;
    private readonly IPersonalDetailsRepository _personalDetailsRepo;
    private readonly IContactDetailsRepository _contactDetailsRepo;
    private readonly IEducationalDetailsRepository _educationalDetailsRepo;
    private readonly IDisabilityLearningSupportRepository _disabilityRepo;
    private readonly IHousingRepository _housingRepo;
    private readonly ISubmissionRepository _submissionRepo;
    private readonly IRepository<Student> _studentdRepository;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

    public ManageController(
        UserManager<Student> userManager,
        IRepository<Student> studentdRepository,
        Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
        IPersonalDetailsRepository personalDetailsRepo,
        IContactDetailsRepository contactDetailsRepo,
        IEducationalDetailsRepository educationalDetailsRepo,
        IDisabilityLearningSupportRepository disabilityRepo,
        IHousingRepository housingRepo,
        ISubmissionRepository submissionRepo)
    {
        _userManager = userManager;
        _studentdRepository = studentdRepository;
        _hostingEnvironment = hostingEnvironment;
        _personalDetailsRepo = personalDetailsRepo;
        _contactDetailsRepo = contactDetailsRepo;
        _educationalDetailsRepo = educationalDetailsRepo;
        _disabilityRepo = disabilityRepo;
        _housingRepo = housingRepo;
        _submissionRepo = submissionRepo;
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

    [HttpGet]
    public IActionResult StudentDetails(string studentId)
    {
        // Fetch necessary data from repositories
        var student = _studentdRepository.GetById(studentId);
        var personalDetails = _personalDetailsRepo.GetByStudentId(studentId);
        var contactDetails = _contactDetailsRepo.GetByStudentId(studentId);
        var educationalDetails = _educationalDetailsRepo.GetByStudentId(studentId);
        var disabilityDetails = _disabilityRepo.GetByStudentId(studentId);
        var housingDetails = _housingRepo.GetByStudentId(studentId);
        var submission = _submissionRepo.GetByStudentId(studentId);

        // Create the view model
        var studentDetailsVM = new StudentDetailsVM
        {
            // Populate student details
            Id = student.Id,
            Title = student?.Title,
            ForeName = student?.ForeName,
            ForeName2 = student?.ForeName2,
            ForeName3 = student?.ForeName3,
            FamilyName = student?.FamilyName,
            DateOfBirth = student?.DateOfBirth ?? DateTime.MinValue,

            // Populate personal details
            SecondForeName = personalDetails?.SecondForeName,
            ThirdForeName = personalDetails?.ThirdForeName,
            PrefferedName = personalDetails?.PrefferedName,
            PreviousFamilyName = personalDetails?.PreviousFamilyName,
            Gender = personalDetails?.Gender,
            UniversityStudentId = personalDetails?.UniversityStudentId,
            CountryOfBirth = personalDetails?.CountryOfBirth,
            Nationality = personalDetails?.Nationality,
            PassportNumber = personalDetails?.PassportNumber,
            DateOfIssue = personalDetails?.DateOfIssue ?? DateTime.MinValue,
            DateOfExpiry = personalDetails?.DateOfExpiry ?? DateTime.MinValue,
            PassportFile = personalDetails?.PassportFile,
            PassportFileName = personalDetails?.PassportFileName,
            PassportFileType = personalDetails?.PassportFileType,

            // Populate contact details
            Country = contactDetails?.Country,
            MobilePhoneNumber = contactDetails?.MobilePhoneNumber,
            OtherPhoneNumber = contactDetails?.OtherPhoneNumber,
            NextOfKinTitle = contactDetails?.NextOfKinTitle,
            NextOfKinForeName = contactDetails?.NextOfKinForeName,
            NextOfKinSurName = contactDetails?.NextOfKinSurName,
            Relationship = contactDetails?.Relationship,
            NextOfKinCountry = contactDetails?.NextOfKinCountry,
            NextOfKinMobilePhone = contactDetails?.NextOfKinMobilePhone,
            NextOfKinOtherPhone = contactDetails?.NextOfKinOtherPhone,
            EmailAddress = contactDetails?.EmailAddress,

            // Populate educational details
            ExchangePartnerInstitution = educationalDetails?.ExchangePartnerInstitution,
            ExactNameOfDegreeProgramme = educationalDetails?.ExactNameOfDegreeProgramme,
            TotalLengthOfDegreeProgrammeInYears = educationalDetails?.TotalLengthOfDegreeProgrammeInYears,
            ExpectedMonthOfGraduation = educationalDetails?.ExpectedMonthOfGraduation,
            ExpectedYearOfGraduation = educationalDetails?.ExpectedYearOfGraduation,
            IsEnglishFirstLanguage = educationalDetails?.IsEnglishFirstLanguage,

            // Populate disability details
            HaveADisablity = disabilityDetails?.HaveADisablity,

            // Populate housing details
            WishToApplyForHousingInUniversity = housingDetails?.WishToApplyForHousingInUniversity,

            // Populate submission details
            AgreeToStatements = submission?.AgreeToStatements.ToString(),
            SubmissionCreated = submission?.SubmissionCreated ?? DateTime.MinValue
        };

        // Check if any required data is null to decide the view to display
        //if (student == null || personalDetails == null || contactDetails == null ||
        //    educationalDetails == null || disabilityDetails == null || housingDetails == null || submission == null)
        //{
        //    return View("StudentDetailsWithoutData", studentDetailsVM); // Display view without data
        //}

        return View(studentDetailsVM); // Display view with populated data
    }

}