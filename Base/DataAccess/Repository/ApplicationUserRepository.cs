using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.IdentityModels;

namespace DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private DataContext _db;

        public ApplicationUserRepository(DataContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _db.ApplicationUsers.Where(u => u.Email == email).FirstAsync();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _db.ApplicationUsers.Where(u => u.Email == userId).FirstAsync();
        }

        public async Task<ApplicationUser> GetUserByName(string name)
        {
            return await _db.ApplicationUsers.Where(u => u.Name == name).FirstAsync();
        }

        public async Task<ApplicationUser> GetUserByPhoneNumber(string phone)
        {
            return await _db.ApplicationUsers.Where(u => u.PhoneNumber == phone).FirstAsync();
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            return await _db.ApplicationUsers.Where(u => u.UserName == userName).FirstAsync();
        }

        public async Task UpdateName(string userId, string name)
        {
            var user = await _db.ApplicationUsers.Where(e => e.Email == userId).FirstAsync();
            user.Name = name;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateUserName(string userId, string userName)
        {
            var user = await _db.ApplicationUsers.Where(e => e.Email == userId).FirstAsync();
            user.UserName = userName;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateEmail(string userId, string email)
        {
            var user = await _db.ApplicationUsers.Where(e => e.Email == userId).FirstAsync();
            user.Email = email;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePhoneNumber(string userId, string phone)
        {
            var user = await _db.ApplicationUsers.Where(e => e.Email == userId).FirstAsync();
            user.PhoneNumber = phone;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}