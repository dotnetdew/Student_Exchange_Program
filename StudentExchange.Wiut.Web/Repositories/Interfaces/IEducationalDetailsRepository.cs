using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IEducationalDetailsRepository : IRepository<EducationalDetails>
    {
        public EducationalDetails GetByStudentId(string studentId);
    }
}
