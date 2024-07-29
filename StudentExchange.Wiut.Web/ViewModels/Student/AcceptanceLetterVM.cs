using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentExchange.Wiut.Web.ViewModels.Student;

public class AcceptanceLetterVM
{
    public string SelectedStudentEmail { get; set; }
    public List<SelectListItem> Students { get; set; }
    public string StudentName { get; set; }
    public string AcademicYear { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ConfirmationDeadline { get; set; }
    public decimal DepositAmount { get; set; }
    public DateTime OrientationDate { get; set; }
    public DateTime HousingApplicationDeadline { get; set; }
    public DateTime RegistrationDeadline { get; set; }
    public string AdmissionsOfficersTitle { get; set; }
}
