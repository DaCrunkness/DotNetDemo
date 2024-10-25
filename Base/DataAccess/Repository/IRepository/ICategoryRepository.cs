using Models.DataModels;

namespace DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        string GetName(int? id);
    }
}