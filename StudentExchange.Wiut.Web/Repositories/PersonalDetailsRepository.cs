using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories;

public class PersonalDetailsRepository : Repository<PersonalDetails>, IPersonalDetailsRepository
{
    public PersonalDetailsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public PersonalDetails GetByStudentId(string studentId)
    {
        return _context.Set<PersonalDetails>().FirstOrDefault(x => x.StudentId == studentId);
    }
}
