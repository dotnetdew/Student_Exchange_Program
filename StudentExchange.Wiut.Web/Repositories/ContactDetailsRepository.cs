using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class ContactDetailsRepository : Repository<ContactDetails>, IContactDetailsRepository
    {
        public ContactDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ContactDetails GetByStudentId(string studentId)
        {
            return _context.Set<ContactDetails>().FirstOrDefault(x => x.StudentId == studentId);
        }
    }
}
