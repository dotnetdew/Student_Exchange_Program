using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Data.Migrations;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class EducationalDetailsRepository : Repository<EducationalDetails>, IEducationalDetailsRepository
    {
        public EducationalDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public EducationalDetails GetByStudentId(string studentId)
        {
            return _context.Set<EducationalDetails>().FirstOrDefault(x => x.StudentId == studentId);
        }
    }
}
