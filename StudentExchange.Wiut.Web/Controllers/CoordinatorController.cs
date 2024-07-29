using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.ViewModels.Student;

namespace StudentExchange.Wiut.Web.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly UserManager<Student> _userManager;
        private readonly IEmailSender _emailSender;

        public CoordinatorController(UserManager<Student> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> SendAcceptanceLetterForm()
        {
            var students = await _userManager.Users.ToListAsync();

            var model = new AcceptanceLetterVM()
            {
                Students = students.Select(s => new SelectListItem
                {
                    Value = s.Email,
                    Text = s.Email
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> SendAcceptanceLetter(AcceptanceLetterVM model)
        {
            var student = await _userManager.FindByEmailAsync(model.SelectedStudentEmail);
            if (student == null)
            {
                return NotFound();
            }

            var acceptanceLetterModel = new AcceptanceLetterVM
            {
                AcademicYear = model.AcademicYear,
                StartDate = model.StartDate,
                ConfirmationDeadline = model.ConfirmationDeadline,
                DepositAmount = model.DepositAmount,
                OrientationDate = model.OrientationDate,
                HousingApplicationDeadline = model.HousingApplicationDeadline,
                RegistrationDeadline = model.RegistrationDeadline,
                StudentName = student.ForeName + " " + student.FamilyName,
                AdmissionsOfficersTitle = model.AdmissionsOfficersTitle
            };
            string emailBody2 = "Accepted";
            string emailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8' />
                <title>Acceptance Letter</title>
            </head>
            <body>
                <table border='0' cellpadding='20' cellspacing='0' width='100%'>
                    <tr>
                        <td align='center'>
                            <h1>WIUT University</h1>
                            <p>12 Istiqbol street, Tashkent, Uzbekistan</p>
                            <p>100047</p>
                            <p>info@wiut.uz</p>
                            <p>+998 71 203 74 74, +998 71 238 74 00</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>{DateTime.Now.ToString("MMMM dd, yyyy")}</p>
                            <p>Dear {acceptanceLetterModel.StudentName},</p>
                            <p>We are pleased to inform you that you have been accepted to WIUT University for the {model.AcademicYear}, commencing on {model.StartDate:MMMM dd, yyyy}. Your application has demonstrated your academic excellence, potential, and alignment with the values and goals of our institution.</p>
                            <p>At WIUT University, we strive to provide an exceptional educational experience, fostering an environment that encourages intellectual growth, critical thinking, and personal development. As a student here, you will have access to our state-of-the-art facilities, diverse academic programs, and a vibrant campus community.</p>
                            <p>Next Steps:</p>
                            <ul>
                                <li><strong>Confirmation of Acceptance:</strong> Please confirm your acceptance by {model.ConfirmationDeadline:MMMM dd, yyyy} by submitting the enclosed acceptance form and a non-refundable deposit of {model.DepositAmount:C}, which will be applied towards your tuition fees.</li>
                                <li><strong>Orientation:</strong> You are invited to attend our orientation program starting on {model.OrientationDate:MMMM dd, yyyy}. This will be an opportunity to meet faculty members, fellow students, and become acquainted with the campus.</li>
                                <li><strong>Housing and Accommodation:</strong> If you require on-campus housing, please complete the housing application form by {model.HousingApplicationDeadline:MMMM dd, yyyy}.</li>
                                <li><strong>Registration and Course Selection:</strong> Detailed instructions for course registration and selection will be sent to you in a separate email. Please ensure you complete your registration by {model.RegistrationDeadline:MMMM dd, yyyy}.</li>
                            </ul>
                            <p>We are excited to welcome you to the WIUT University community and look forward to supporting you in achieving your academic and personal goals. Should you have any questions or require further information, please do not hesitate to contact our admissions office at info@wiut.uz or (+998 71) 238 74 00, (+998 71) 238 74 45.</p>
                            <p>Congratulations once again on your acceptance to WIUT University. We are confident that you will make a significant contribution to our campus and look forward to seeing you thrive in your studies.</p>
                            <p>Yours sincerely,</p>
                            <p>{model.AdmissionsOfficersTitle}</p>
                            <p>WIUT University</p>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

            await _emailSender.SendEmailAsync(student.Email, "Acceptance to WIUT University", emailBody);

            return Json(new { success = true });
        }
    }
}
