using Microsoft.AspNetCore.Mvc.Rendering;
using Models.IdentityModels;

namespace Models.ViewModels
{

    public class RoleManagmentVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}