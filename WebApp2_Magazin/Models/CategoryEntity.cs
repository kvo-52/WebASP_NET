namespace WebApp2_Magazin.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ProductEntity?> Products { get; set; } = new List<ProductEntity>();
    }
}
