using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Abstraction

{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        int AddCategory(CategoryDto category);
    }
}
