using Microsoft.AspNetCore.Authorization;

namespace Web.Configurations.Policies
{
    public class AdminWithMoreThan1000DaysRequirement : IAuthorizationRequirement
    {
        public AdminWithMoreThan1000DaysRequirement(int days)
        {
            Days = days;
        }
        public int Days { get; set; }
    }
}