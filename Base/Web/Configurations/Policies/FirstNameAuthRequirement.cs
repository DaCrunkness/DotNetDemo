using Microsoft.AspNetCore.Authorization;

namespace Web.Configurations.Policies
{
    public class FirstNameAuthRequirement : IAuthorizationRequirement
    {
        public FirstNameAuthRequirement(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}