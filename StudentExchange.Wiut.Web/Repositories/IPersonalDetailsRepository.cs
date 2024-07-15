using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories
{
    public interface IPersonalDetailsRepository : IRepository<PersonalDetails>
    {
        public IList<PersonalDetails> GetAllWithStudentId(int studentId);
    }
}
