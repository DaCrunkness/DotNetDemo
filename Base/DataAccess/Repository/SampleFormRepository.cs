using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.DataModels;

namespace DataAccess.Repository
{
    public class SampleFormRepository : Repository<SampleForm>, ISampleFormRepository
    {
        private DataContext _db;
        public SampleFormRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SampleForm sampleForm)
        {
            _db.SampleForms.Update(sampleForm);
        }
    }
}