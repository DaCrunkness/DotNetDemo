using AutoMapper;
using Models.DataModels;
using Models.ViewModels;

namespace Web.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
        }
    }
}