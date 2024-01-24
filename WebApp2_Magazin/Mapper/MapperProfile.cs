using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using WebApp2_Magazin.Models.DTO;
using WebApp2_Magazin.Models;

namespace WebApp2_Magazin.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<StorageEntity, StorageDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        }
    }
}
