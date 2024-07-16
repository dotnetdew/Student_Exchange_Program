using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IDisabilityLearningSupportRepository : IRepository<DisabilityLearningSupport>
    {
        public DisabilityLearningSupport GetByStudentId(string studentId);
    }
}
