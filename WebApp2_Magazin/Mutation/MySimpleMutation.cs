using HotChocolate;
using WebApp2_Magazin.Abstraction;
using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Mutation
{
    public class MySimpleMutation
    {
        public int AddProduct(ProductDto product, [Service] IProductService service)
        {
            var id = service.AddProduct(product);
            return id;
        }
    }
}
