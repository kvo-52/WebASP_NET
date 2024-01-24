using HotChocolate;
using WebApp2_Magazin.Abstraction;
using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService service) => service.GetProducts();
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService service) => service.GetStorages();
        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService service) => service.GetCategories();
    }
}
