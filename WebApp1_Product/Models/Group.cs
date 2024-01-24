namespace WebApp1_Product.Models
{
    public class Group: BaseModel
    {
        public virtual List<Product> Products { get; set; } = null!;
    }
}
