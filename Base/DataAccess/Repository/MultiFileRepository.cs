using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.DataModels;

namespace DataAccess.Repository
{
    public class MultiFileRepository : Repository<MultiFile>, IMultiFileRepository
    {
        private DataContext _db;
        public MultiFileRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MultiFile multiFile)
        {
            _db.MultiFiles.Update(multiFile);
        }
    }
}