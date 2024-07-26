using StudentExchange.Wiut.Web.Models;

namespace StudentExchange.Wiut.Web.Repositories.Interfaces
{
    public interface IFileDetailRepository : IRepository<FileDetail>
    {
        public void RemoveRange(IEnumerable<FileDetail> files);
    }
}
