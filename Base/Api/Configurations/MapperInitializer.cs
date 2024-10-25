using AutoMapper;
using Models.DataModels;
using Models.IdentityModels;
using Models.ViewModels;

namespace Api.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApplicationUser, ApiUser>().ReverseMap();
            CreateMap<ApplicationUser, ApiRegister>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
        }
    }
}