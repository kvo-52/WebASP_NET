using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Abstraction
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
    }
}
