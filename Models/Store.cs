namespace WebApp1_Product.Models
{
    public class Store : BaseModel
    {
        public virtual List<Product> Products { get; set; } = null!;
        public int Count { get; set; }
    }
}
