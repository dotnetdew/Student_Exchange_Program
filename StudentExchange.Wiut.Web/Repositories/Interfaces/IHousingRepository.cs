using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IHousingRepository : IRepository<Housing>
    {
        public Housing GetByStudentId(string studentId);
    }
}
