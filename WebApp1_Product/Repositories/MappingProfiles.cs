using AutoMapper;
using WebApp1_Product.Models.DTO;
using WebApp1_Product.Models;

namespace WebApp1_Product.Repositories
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, DTOProduct>(MemberList.Destination).ReverseMap();
            CreateMap<Group, DTOGroup>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreDto>(MemberList.Destination).ReverseMap();
        }
    }
}
