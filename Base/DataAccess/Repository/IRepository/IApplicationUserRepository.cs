using Models.IdentityModels;

namespace DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);
        Task<ApplicationUser> GetUserByName(string name);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserByPhoneNumber(string phone);
        Task<ApplicationUser> GetUserByUserName(string userName);
        Task UpdateName(string user, string name);
        Task UpdateUserName(string user, string userName);
        Task UpdateEmail(string user, string email);
        Task UpdatePhoneNumber(string user, string phone);

    }
}