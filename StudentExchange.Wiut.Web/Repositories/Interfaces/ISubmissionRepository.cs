using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface ISubmissionRepository : IRepository<Submission>
    {
        public Submission GetByStudentId(string studentId);
    }
}
