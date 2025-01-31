﻿using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models.IdentityModels;
using System.Security.Claims;

namespace Api.Configurations.Policies
{
    public class FirstNameAuthHandler : AuthorizationHandler<FirstNameAuthRequirement>
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        public DataContext _db { get; set; }

        public FirstNameAuthHandler(UserManager<ApplicationUser> userManager, DataContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FirstNameAuthRequirement requirement)
        {
            string userid = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userid);
            var claims = Task.Run(async () => await _userManager.GetClaimsAsync(user)).Result;
            var claim = claims.FirstOrDefault(c => c.Type == "FirstName");
            if (claim != null)
            {
                if (claim.Value.ToLower().Contains(requirement.Name.ToLower()))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}