using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class HousingRepository : Repository<Housing>, IHousingRepository
    {
        public HousingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Housing GetByStudentId(string studentId)
        {
            return _context.Set<Housing>().FirstOrDefault(x => x.StudentId == studentId);
        }
    }
}
