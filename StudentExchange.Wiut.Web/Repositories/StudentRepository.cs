using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Data;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
