using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories;
using StudentExchange.Wiut.Web.ViewModels;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
    private readonly IRepository<Submission> _submissionRepository;
    private readonly IEmailSender _emailSender;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

    public StudentsController(
        UserManager<Student> userManager, 
        IRepository<PersonalDetails> personalDetailsRepository,
        IRepository<ContactDetails> contactDetailsRepository,
        IRepository<Student> studentdRepository,
        IRepository<EducationalDetails> educationalDetailsRepository,
        IRepository<DisabilityLearningSupport> disabilityLearningSupportRepository,
        IRepository<Housing> housingRepository,
        IRepository<Submission> submissionRepository,
        IEmailSender emailSender,
        Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        _userManager = userManager;
        _personalDetailsRepository = personalDetailsRepository;
        _studentdRepository = studentdRepository;
        _contactDetailsRepository = contactDetailsRepository;
        _educationalDetailsRepository = educationalDetailsRepository;
        _disabilityLearningSupportRepository = disabilityLearningSupportRepository;
        _housingRepository = housingRepository;
        _submissionRepository = submissionRepository;
        _emailSender = emailSender;
        _hostingEnvironment = hostingEnvironment;
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
        var personalVM = new CreatePersonalDetailsVM()
        {
            Title = student.Title,
            ForeName = student.ForeName,
            SecondForeName = student.ForeName2,
            ThirdForeName = student.ForeName3,
            FamilyName = student.FamilyName,
            DateOfBirth = student.DateOfBirth,
            StudentId = studentId
        };
        return View(personalVM);
    }

    [HttpPost]
    public IActionResult SavePersonalDetails(CreatePersonalDetailsVM vm)
    {
        //var personalDetails = _personalDetailsRepository.GetById
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
                    StudentId = vm.StudentId,
                };
                _personalDetailsRepository.Add(personalDetails);
                _personalDetailsRepository.Save();

                ViewBag.Saved = "true";

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
                OtherPhoneNumber = vm.OtherPhoneNumber2,
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

            ViewBag.Saved = "true";

            return RedirectToAction("SaveEducationalDetails", new { studentId = vm.StudentId });
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

            ViewBag.Saved = "true";

            return RedirectToAction("SaveDisabilityLearningSupport", new { studentId = vm.StudentId });
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

            ViewBag.Saved = "true";

            return RedirectToAction("SaveHousingDetails", new { studentId = vm.StudentId });
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

            ViewBag.Saved = "true";

            return RedirectToAction("SaveSubmission", new { studentId = vm.StudentId });
        }
        else
            return View();
    }

    [HttpGet]
    public IActionResult SaveSubmission(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var submissionVM = new CreateSubmissionVM()
        {
            StudentId = studentId,
            EmailAddress = student.Email
        };
        return View(submissionVM);
    }

    [HttpPost]
    public IActionResult SaveSubmission(CreateSubmissionVM vm)
    {
        var student = _studentdRepository.GetById(vm.StudentId);
        if (ModelState.IsValid)
        {
            var submission = new Submission()
            {
                StudentId = vm.StudentId,
                SubmissionCreated = DateTime.Now,
                AgreeToStatements = vm.AgreeToStatements
            };

            _submissionRepository.Add(submission);
            _submissionRepository.Save();

            string emailBody = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Student Exchange Offer</title>
            </head>
            <body>
                <img src='cid:email_west.png' alt='WIUT Logo' />
                <p>Dear {StudentName},</p>
                <p>It is our pleasure to inform you that you have been accepted onto the Westminster International University in Tashkent Exchange Programme.</p>
                <h3>Exchange Programme Details:</h3>
                <p>Academic Year: 2024/25</p>
                <p>Duration: One semester</p>
                <p>Key Dates:</p>
                <ul>
                    <li>Orientation Week: 9 – 13 September 2024</li>
                    <li>Semester 1 duration: 16 September – 27 December 2024</li>
                </ul>
                <h3>What to do next?</h3>
                <h4>Pre-Arrival Information</h4>
                <p>The International Office will be sending you a pre-arrival e-mail to assist you with preparing for your upcoming semester at WIUT shortly. The information covered will include information to help you prepare for the accommodation and visa information student life in Uzbekistan and at WIUT.</p>
                <h4>Terms and Conditions</h4>
                <p>You should note details of our Inbound Student Exchange Terms and Conditions which can be found at the bottom of the page.</p>
                <h4>Module and Results</h4>
                <p>There is a set module pathway for exchange students nominated on the Economics subject area. You will require to contact either the Student Exchange Coordinator of your home campus or WIUT Course Leader of your subject area for advice on module selection.</p>
                <p>Please note that as a semester-long student you will not receive semester 1 results until the result release date after semester 2.</p>
                <h4>Disability Learning Support</h4>
                <p>WIUT welcomes students with conditions or disabilities. We have an Inclusivity and Diversity Officer who can provide confidential support to help you make the most of your studies during your study abroad experience.</p>
                <p>If you did not declare a disability at the time of application and now wish to register please contact us via email on sang@wiut.uz so that we can provide you with further information on how to register.</p>
                <h4>Comprehensive Medical and Travel Insurance</h4>
                <p>All students should ensure that they have comprehensive private medical and travel insurance to cover their time in Uzbekistan.</p>
                <p>We look forward to welcoming you to WIUT!</p>
                <p>Kind regards,</p>
                <p>Student Records</p>
                <p>Academic Registrar’s Office</p>
                <p>Westminster International University in Tashkent</p>
            </body>
            </html>";

            string studentName = student.ForeName + " " + student.FamilyName;
            emailBody = emailBody.Replace("{StudentName}", studentName);

            string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "email_west.png");

            // Create the alternate view for the email body
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, MediaTypeNames.Text.Html);

            // Add the image to the email
            LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Png)
            {
                ContentId = "email_west.png"
            };
            htmlView.LinkedResources.Add(logo);


            MailMessage message = new MailMessage();
            message.From = new MailAddress("exchange@wiut.uz", "Westminster University in Tashkent");
            message.To.Add(vm.EmailAddress);
            message.Subject = "Submission";
            message.Body = string.Empty;
            message.IsBodyHtml = true;
            message.AlternateViews.Add(htmlView);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "klms.wiut.uz";
            smtpClient.Port = 27;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("exchange@wiut.uz", "2873KLMS365");
            smtpClient.Timeout = 30000;

            smtpClient.Send(message);

            ViewBag.Saved = "true";

            return View(vm);
        }
        else
            return View();
    }
}
