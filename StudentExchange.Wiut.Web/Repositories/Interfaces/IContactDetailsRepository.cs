using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IContactDetailsRepository : IRepository<ContactDetails>
    {
        public ContactDetails GetByStudentId(string studentId);
    }
}
