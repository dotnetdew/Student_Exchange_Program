using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories.Interfaces;

namespace StudentExchange.Wiut.Web.Repositories
{
    public class FileDetailRepository : Repository<FileDetail>, IFileDetailRepository
    {
        public FileDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void RemoveRange(IEnumerable<FileDetail> files)
        {
            _context.FileDetails.RemoveRange(files);
        }
    }
}
