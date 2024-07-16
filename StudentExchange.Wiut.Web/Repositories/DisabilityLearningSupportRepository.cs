using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class DisabilityLearningSupportRepository : Repository<DisabilityLearningSupport>, IDisabilityLearningSupportRepository
    {
        public DisabilityLearningSupportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DisabilityLearningSupport GetByStudentId(string studentId)
        {
            return _context.Set<DisabilityLearningSupport>().FirstOrDefault(x => x.StudentId == studentId);
        }
    }
}
