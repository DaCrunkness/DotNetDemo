using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.DataModels;

namespace DataAccess.Repository
{
    public class MultiFileFormRepository : Repository<MultiFileForm>, IMultiFileFormRepository
    {
        private DataContext _db;
        public MultiFileFormRepository(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}