﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;
using StudentExchange.Wiut.Web.ViewModels;
using System.Security.Claims;

namespace StudentExchange.Wiut.Web.Controllers;

[Authorize]
public class StudentsController : Controller
{
    private readonly UserManager<Student> _userManager;
    private readonly IPersonalDetailsRepository _personalDetailsRepo;
    private readonly IContactDetailsRepository _contactDetailsRepo;
    private readonly IEducationalDetailsRepository _educationalDetailsRepo;
    private readonly IDisabilityLearningSupportRepository _disabilityRepo;
    private readonly IHousingRepository _housingRepo;
    private readonly ISubmissionRepository _submissionRepo;
    private readonly IRepository<Student> _studentdRepository;
    private readonly IEmailSender _emailSender;
    private readonly IFileDetailRepository _fileDetailRepo;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

    public StudentsController(
        UserManager<Student> userManager, 
        IRepository<Student> studentdRepository,
        IEmailSender emailSender,
        Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
        IPersonalDetailsRepository personalDetailsRepo,
        IContactDetailsRepository contactDetailsRepo,
        IEducationalDetailsRepository educationalDetailsRepo,
        IDisabilityLearningSupportRepository disabilityRepo,
        IHousingRepository housingRepo,
        ISubmissionRepository submissionRepo,
        IFileDetailRepository fileDetailRepo)
    {
        _userManager = userManager;
        _studentdRepository = studentdRepository;
        _emailSender = emailSender;
        _hostingEnvironment = hostingEnvironment;
        _personalDetailsRepo = personalDetailsRepo;
        _contactDetailsRepo = contactDetailsRepo;
        _educationalDetailsRepo = educationalDetailsRepo;
        _disabilityRepo = disabilityRepo;
        _housingRepo = housingRepo;
        _submissionRepo = submissionRepo;
        _fileDetailRepo = fileDetailRepo;
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
    public IActionResult DisplayPassport(string studentId)
    {
        var personalDetail = _personalDetailsRepo.GetByStudentId(studentId);

        if (personalDetail != null && personalDetail.PassportFile != null)
            return File(personalDetail.PassportFile, personalDetail.PassportFileType, personalDetail.PassportFileName);
        else
            return new EmptyResult();
    }

    [HttpGet]
    public IActionResult SavePersonalDetails(string studentId)
    {
        var existingPersonalDetails = _personalDetailsRepo.GetByStudentId(studentId);

        var personalVM = new CreatePersonalDetailsVM()
        {
            StudentId = studentId
        };

        if (existingPersonalDetails != null)
        {
            personalVM.Title = existingPersonalDetails.Title;
            personalVM.ForeName = existingPersonalDetails.ForeName;
            personalVM.SecondForeName = existingPersonalDetails.SecondForeName;
            personalVM.ThirdForeName = existingPersonalDetails.ThirdForeName;
            personalVM.FamilyName = existingPersonalDetails.FamilyName;
            personalVM.DateOfBirth = existingPersonalDetails.DateOfBirth;
            personalVM.CountryOfBirth = existingPersonalDetails.CountryOfBirth;
            personalVM.DateOfIssue = existingPersonalDetails.DateOfIssue;
            personalVM.DateOfExpiry = existingPersonalDetails.DateOfExpiry;
            personalVM.Gender = existingPersonalDetails.Gender;
            personalVM.Nationality = existingPersonalDetails.Nationality;
            personalVM.PassportNumber = existingPersonalDetails.PassportNumber;
            personalVM.PrefferedName = existingPersonalDetails.PrefferedName;
            personalVM.PreviousFamilyName = existingPersonalDetails.PreviousFamilyName;
            personalVM.UniversityStudentId = existingPersonalDetails.UniversityStudentId;
        }

        return View(personalVM);
    }


    [HttpPost]
    public IActionResult SavePersonalDetails(CreatePersonalDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            var existingPersonalDetails = _personalDetailsRepo.GetByStudentId(vm.StudentId);

            if (existingPersonalDetails != null)
            {
                using (var stream = new MemoryStream())
                {
                    vm.PassportFile.CopyTo(stream);

                    existingPersonalDetails.Title = vm.Title;
                    existingPersonalDetails.ForeName = vm.ForeName;
                    existingPersonalDetails.SecondForeName = vm.SecondForeName;
                    existingPersonalDetails.ThirdForeName = vm.ThirdForeName;
                    existingPersonalDetails.FamilyName = vm.FamilyName;
                    existingPersonalDetails.PrefferedName = vm.PrefferedName;
                    existingPersonalDetails.PreviousFamilyName = vm.PreviousFamilyName;
                    existingPersonalDetails.DateOfBirth = vm.DateOfBirth;
                    existingPersonalDetails.Gender = vm.Gender;
                    existingPersonalDetails.UniversityStudentId = vm.UniversityStudentId;
                    existingPersonalDetails.CountryOfBirth = vm.CountryOfBirth;
                    existingPersonalDetails.Nationality = vm.Nationality;
                    existingPersonalDetails.PassportNumber = vm.PassportNumber;
                    existingPersonalDetails.DateOfIssue = vm.DateOfIssue;
                    existingPersonalDetails.DateOfExpiry = vm.DateOfExpiry;
                    existingPersonalDetails.PassportFile = stream.ToArray();
                    existingPersonalDetails.PassportFileName = vm.PassportFile.FileName;
                    existingPersonalDetails.PassportFileType = vm.PassportFile.ContentType;

                    var existingFiles = existingPersonalDetails.FileDetails.Where(fd => fd.PersonalDetailsId == existingPersonalDetails.Id).ToList();
                    _fileDetailRepo.RemoveRange(existingFiles);

                    foreach (var file in vm.MultipleFiles)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new MemoryStream())
                            {
                                file.CopyTo(fileStream);

                                var fileDetail = new FileDetail()
                                {
                                    FileName = file.FileName,
                                    FileType = file.ContentType,
                                    Content = fileStream.ToArray(),
                                    PersonalDetailsId = existingPersonalDetails.Id
                                };

                                _fileDetailRepo.Add(fileDetail);
                            }
                        }
                    }

                    _personalDetailsRepo.Update(existingPersonalDetails);
                    _personalDetailsRepo.Save();
                }
            }
            else
            {
                // Create new PersonalDetails if none exists
                using (var stream = new MemoryStream())
                {
                    vm.PassportFile.CopyTo(stream);
                    var newPersonalDetails = new PersonalDetails()
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

                    foreach (var file in vm.MultipleFiles)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new MemoryStream())
                            {
                                file.CopyTo(fileStream);

                                var fileDetail = new FileDetail()
                                {
                                    FileName = file.FileName,
                                    FileType = file.ContentType,
                                    Content = fileStream.ToArray(),
                                    PersonalDetailsId = existingPersonalDetails.Id
                                };

                                _fileDetailRepo.Add(fileDetail);
                            }
                        }
                    }

                    _personalDetailsRepo.Add(newPersonalDetails);
                    _personalDetailsRepo.Save();
                }
            }

            ViewBag.Saved = true;
            return RedirectToAction("SaveContactDetails", new { studentId = vm.StudentId });
        }

        // If ModelState is not valid, return to the view with validation errors
        return View(vm);
    }


    [HttpGet]
    public IActionResult SaveContactDetails(string studentId)
    {
        //var student = _studentRepository.GetById(studentId);
        var existingContactDetails = _contactDetailsRepo.GetByStudentId(studentId);

        var contactVM = new CreateContactDetailsVM()
        {
            StudentId = studentId
        };

        if (existingContactDetails != null)
        {
            contactVM.Country = existingContactDetails.Country;
            contactVM.MobilePhoneNumber = existingContactDetails.MobilePhoneNumber;
            contactVM.OtherPhoneNumber2 = existingContactDetails.OtherPhoneNumber;
            contactVM.NextOfKinTitle = existingContactDetails.NextOfKinTitle;
            contactVM.NextOfKinForeName = existingContactDetails.NextOfKinForeName;
            contactVM.NextOfKinSurName = existingContactDetails.NextOfKinSurName;
            contactVM.Relationship = existingContactDetails.Relationship;
            contactVM.NextOfKinCountry = existingContactDetails.NextOfKinCountry;
            contactVM.NextOfKinMobilePhone = existingContactDetails.NextOfKinMobilePhone;
            contactVM.NextOfKinOtherPhone = existingContactDetails.NextOfKinOtherPhone;
            contactVM.EmailAddress = existingContactDetails.EmailAddress;
        }

        return View(contactVM);
    }



    [HttpPost]
    public IActionResult SaveContactDetails(CreateContactDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            var existingContactDetails = _contactDetailsRepo.GetByStudentId(vm.StudentId);

            if (existingContactDetails == null)
            {
                // Create new contact details object
                var contactDetails = new ContactDetails
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

                _contactDetailsRepo.Add(contactDetails);
            }
            else
            {
                // Update existing contact details object
                existingContactDetails.Country = vm.Country;
                existingContactDetails.MobilePhoneNumber = vm.MobilePhoneNumber;
                existingContactDetails.OtherPhoneNumber = vm.OtherPhoneNumber2;
                existingContactDetails.NextOfKinTitle = vm.NextOfKinTitle;
                existingContactDetails.NextOfKinForeName = vm.NextOfKinForeName;
                existingContactDetails.NextOfKinSurName = vm.NextOfKinSurName;
                existingContactDetails.Relationship = vm.Relationship;
                existingContactDetails.NextOfKinCountry = vm.NextOfKinCountry;
                existingContactDetails.NextOfKinMobilePhone = vm.NextOfKinMobilePhone;
                existingContactDetails.NextOfKinOtherPhone = vm.NextOfKinOtherPhone;
                existingContactDetails.EmailAddress = vm.EmailAddress;

                _contactDetailsRepo.Update(existingContactDetails);
            }

            _contactDetailsRepo.Save();

            ViewBag.Saved = true;

            return RedirectToAction("SaveEducationalDetails", new { studentId = vm.StudentId });
        }

        return View(vm);
    }


    [HttpGet]
    public IActionResult SaveEducationalDetails(string studentId)
    {
        //var student = _studentRepository.GetById(studentId);
        var existingEducationalDetails = _educationalDetailsRepo.GetByStudentId(studentId);

        var educationalVM = new CreateEducationalDetailsVM()
        {
            StudentId = studentId
        };

        if (existingEducationalDetails != null)
        {
            educationalVM.ExchangePartnerInstitution = existingEducationalDetails.ExchangePartnerInstitution;
            educationalVM.ExactNameOfDegreeProgramme = existingEducationalDetails.ExactNameOfDegreeProgramme;
            educationalVM.TotalLengthOfDegreeProgrammeInYears = existingEducationalDetails.TotalLengthOfDegreeProgrammeInYears;
            educationalVM.ExpectedMonthOfGraduation = existingEducationalDetails.ExpectedMonthOfGraduation;
            educationalVM.ExpectedYearOfGraduation = existingEducationalDetails.ExpectedYearOfGraduation;
            educationalVM.IsEnglishFirstLanguage = existingEducationalDetails.IsEnglishFirstLanguage;
        }

        return View(educationalVM);
    }


    [HttpPost]
    public IActionResult SaveEducationalDetails(CreateEducationalDetailsVM vm)
    {
        if (ModelState.IsValid)
        {
            var existingEducationalDetails = _educationalDetailsRepo.GetByStudentId(vm.StudentId);

            if (existingEducationalDetails != null)
            {
                // Update existing educational details
                existingEducationalDetails.ExchangePartnerInstitution = vm.ExchangePartnerInstitution;
                existingEducationalDetails.ExactNameOfDegreeProgramme = vm.ExactNameOfDegreeProgramme;
                existingEducationalDetails.TotalLengthOfDegreeProgrammeInYears = vm.TotalLengthOfDegreeProgrammeInYears;
                existingEducationalDetails.ExpectedMonthOfGraduation = vm.ExpectedMonthOfGraduation;
                existingEducationalDetails.ExpectedYearOfGraduation = vm.ExpectedYearOfGraduation;
                existingEducationalDetails.IsEnglishFirstLanguage = vm.IsEnglishFirstLanguage;

                _educationalDetailsRepo.Update(existingEducationalDetails);
            }
            else
            {
                // Create new educational details
                var newEducationalDetails = new EducationalDetails()
                {
                    ExchangePartnerInstitution = vm.ExchangePartnerInstitution,
                    ExactNameOfDegreeProgramme = vm.ExactNameOfDegreeProgramme,
                    TotalLengthOfDegreeProgrammeInYears = vm.TotalLengthOfDegreeProgrammeInYears,
                    ExpectedMonthOfGraduation = vm.ExpectedMonthOfGraduation,
                    ExpectedYearOfGraduation = vm.ExpectedYearOfGraduation,
                    IsEnglishFirstLanguage = vm.IsEnglishFirstLanguage,
                    StudentId = vm.StudentId
                };

                _educationalDetailsRepo.Add(newEducationalDetails);
            }

            _educationalDetailsRepo.Save();

            ViewBag.Saved = "true";

            return RedirectToAction("SaveDisabilityLearningSupport", new { studentId = vm.StudentId });
        }
        else
        {
            return View(vm);
        }
    }


    [HttpGet]
    public IActionResult SaveDisabilityLearningSupport(string studentId)
    {
        //var student = _studentdRepository.GetById(studentId);
        var existingDisabilityDetails = _disabilityRepo.GetByStudentId(studentId);

        var disabilityLearningVM = new CreateDisabilityLearningSupportVM()
        {
            StudentId = studentId
        };

        if (existingDisabilityDetails != null)
        {
            disabilityLearningVM.HaveADisablity = existingDisabilityDetails.HaveADisablity;
        }

        return View(disabilityLearningVM);
    }


    [HttpPost]
    public IActionResult SaveDisabilityLearningSupport(CreateDisabilityLearningSupportVM vm)
    {
        if (ModelState.IsValid)
        {
            var existingDisabilityDetails = _disabilityRepo.GetByStudentId(vm.StudentId);

            if (existingDisabilityDetails != null)
            {
                // Update existing disability details
                existingDisabilityDetails.HaveADisablity = vm.HaveADisablity;

                _disabilityRepo.Update(existingDisabilityDetails);
            }
            else
            {
                // Create new disability details
                var newDisabilityDetails = new DisabilityLearningSupport()
                {
                    HaveADisablity = vm.HaveADisablity,
                    StudentId = vm.StudentId
                };

                _disabilityRepo.Add(newDisabilityDetails);
            }

            _disabilityRepo.Save();

            ViewBag.Saved = "true";

            return RedirectToAction("SaveHousingDetails", new { studentId = vm.StudentId });
        }
        else
        {
            return View(vm);
        }
    }


    [HttpGet]
    public IActionResult SaveHousingDetails(string studentId)
    {
        //var student = _studentdRepository.GetById(studentId);
        var existingHousing = _housingRepo.GetByStudentId(studentId);

        var housingVM = new CreateHousingVM()
        {
            StudentId = studentId
        };

        if (existingHousing != null)
        {
            housingVM.WishToApplyForHousingInUniversity = existingHousing.WishToApplyForHousingInUniversity;
        }

        return View(housingVM);
    }


    [HttpPost]
    public IActionResult SaveHousingDetails(CreateHousingVM vm)
    {
        if (ModelState.IsValid)
        {
            var existingHousing = _housingRepo.GetByStudentId(vm.StudentId);

            if (existingHousing != null)
            {
                existingHousing.WishToApplyForHousingInUniversity = vm.WishToApplyForHousingInUniversity;
                _housingRepo.Update(existingHousing);
            }
            else
            {
                var housing = new Housing()
                {
                    WishToApplyForHousingInUniversity = vm.WishToApplyForHousingInUniversity,
                    StudentId = vm.StudentId
                };

                _housingRepo.Add(housing);
            }

            _housingRepo.Save();

            ViewBag.Saved = "true";

            return RedirectToAction("SaveSubmission", new { studentId = vm.StudentId });
        }
        else
        {
            return View();
        }
    }


    [HttpGet]
    public IActionResult SaveSubmission(string studentId)
    {
        var student = _studentdRepository.GetById(studentId);
        var existingSubmission = _submissionRepo.GetByStudentId(studentId);
        var personalDetail = _personalDetailsRepo.GetByStudentId(studentId);

        var submissionVM = new CreateSubmissionVM()
        {
            StudentId = studentId,
            EmailAddress = student.Email
        };

        if (existingSubmission != null)
        {
            submissionVM.AgreeToStatements = existingSubmission.AgreeToStatements;
            submissionVM.SubmissionCreated = existingSubmission.SubmissionCreated;
        }

        ViewBag.PassportFile = personalDetail.PassportFile;

        return View(submissionVM);
    }

    [HttpPost]
    public IActionResult SaveSubmission(CreateSubmissionVM vm)
    {
        var student = _studentdRepository.GetById(vm.StudentId);

        if (ModelState.IsValid)
        {
            var existingSubmission = _submissionRepo.GetByStudentId(vm.StudentId);

            if (existingSubmission != null)
            {
                existingSubmission.AgreeToStatements = vm.AgreeToStatements;
                existingSubmission.SubmissionCreated = DateTime.Now; // Assuming you want to update this date on every submission
                _submissionRepo.Update(existingSubmission);
            }
            else
            {
                var submission = new Submission()
                {
                    StudentId = vm.StudentId,
                    AgreeToStatements = vm.AgreeToStatements,
                    SubmissionCreated = DateTime.Now // Assuming this is the first submission date
                };

                _submissionRepo.Add(submission);
            }

            _submissionRepo.Save();

            string StudentName = student.ForeName + " " + student.FamilyName;
            string emailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Submission of application</title>
            </head>
            <body>
                <p>Dear {StudentName},</p>
                <p>Your Student Exchange Programme application to the Westminster International University in Tashkent has been submitted. We will be in touch regarding the outcome of your application.</p>
                <p>As always, please do not hesitate to contact us via email if any other issues arise concerning your application.</p>
                <p>With best wishes,</p>
                <p>Sharon (Seow Yee) Ang<br>
                Senior Officer on International Studies<br>
                <a href='mailto:sang@wiut.uz'>sang@wiut.uz</a></p>
                <p>Westminster International University in Tashkent</p>
            </body>
            </html>";

            _emailSender.SendEmailAsync(vm.EmailAddress, "Submission of application", emailBody);

            ViewBag.Saved = "true";

            return View(vm);
        }
        else
        {
            return View(vm);
        }
    }
}
