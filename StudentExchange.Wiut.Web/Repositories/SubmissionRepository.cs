using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Submission GetByStudentId(string studentId)
        {
            return _context.Set<Submission>().FirstOrDefault(x => x.StudentId == studentId);
        }
    }
}
