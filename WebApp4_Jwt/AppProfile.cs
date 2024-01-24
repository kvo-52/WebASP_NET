using AutoMapper;
using WebApp2_Magazin.Models;
using WebApp2_Magazin.Models.DTO;


namespace WebApp4_Jwt
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductEntity, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<StorageEntity, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
