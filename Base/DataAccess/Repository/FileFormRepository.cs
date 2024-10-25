using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.DataModels;

namespace DataAccess.Repository
{
    public class FileFormRepository : Repository<FileForm>, IFileFormRepository
    {
        private DataContext _db;
        public FileFormRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FileForm fileForm)
        {
            _db.FileForms.Update(fileForm);
        }
    }
}