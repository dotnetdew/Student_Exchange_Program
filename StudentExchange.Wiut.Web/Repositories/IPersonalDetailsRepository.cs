using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories
{
    public interface IPersonalDetailsRepository : IRepository<PersonalDetails>
    {
        public IQueryable<PersonalDetails> GetAllWithStudentId(string studentId);
    }
}
