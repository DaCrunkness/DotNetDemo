﻿using DataAccess.Data;

namespace Web.Configurations.Policies
{
    public class NumberOfDaysForAccount : INumberOfDaysForAccount
    {
        private readonly DataContext _db;
        public NumberOfDaysForAccount(DataContext db)
        {
            _db = db;
        }

        public int Get(string userId)
        {
            //var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            //if (user != null && user.DateCreated != DateTime.MinValue)
            //{
            //    return (DateTime.Today - user.DateCreated).Days;
            //}
            return 0;
        }
    }

}