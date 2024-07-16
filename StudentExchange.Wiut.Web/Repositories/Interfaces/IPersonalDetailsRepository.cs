using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IPersonalDetailsRepository : IRepository<PersonalDetails>
    {
        public PersonalDetails GetByStudentId(string studentId);
    }
}
