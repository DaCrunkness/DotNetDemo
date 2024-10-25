using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _db;

        public UnitOfWork(DataContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            SampleForm = new SampleFormRepository(_db);
            FileForm = new FileFormRepository(_db);
            MultiFile = new MultiFileRepository(_db);
            MultiFileForm = new MultiFileFormRepository(_db);
            Category = new CategoryRepository(_db);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public ICategoryRepository Category { get; private set; }
        public ISampleFormRepository SampleForm { get; private set; }
        public IFileFormRepository FileForm { get; private set; }
        public IMultiFileRepository MultiFile { get; private set; }
        public IMultiFileFormRepository MultiFileForm { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
    }
}