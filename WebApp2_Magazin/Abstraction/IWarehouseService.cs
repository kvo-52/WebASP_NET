using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Abstraction
{
    public interface IWarehouseService
    {
        List<WarehouseItem> GetItems(int warehouseId);
    }
}
