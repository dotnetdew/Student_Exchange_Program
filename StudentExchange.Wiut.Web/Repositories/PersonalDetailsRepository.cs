using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories;

public class PersonalDetailsRepository : Repository<PersonalDetails>, IPersonalDetailsRepository
{
    public PersonalDetailsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IList<PersonalDetails> GetAllWithStudentId(int studentId)
    {
        return _context.Set<PersonalDetails>().Where(x => x.StudentId == studentId.ToString()).ToList();
    }
}
