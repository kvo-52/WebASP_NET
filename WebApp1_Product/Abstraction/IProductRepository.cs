using WebApp1_Product.Models.DTO;

namespace WebApp1_Product.Abstraction
{
    public interface IProductRepository
    {
        public int AddProduct(DTOProduct product);
        public string GetProductsCSV();
        public string GetСacheStatCSV();

        public IEnumerable<DTOProduct> GetProducts();

        
    }
}
