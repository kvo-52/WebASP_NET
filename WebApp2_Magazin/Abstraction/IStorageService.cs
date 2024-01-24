using WebApp2_Magazin.Models.DTO;

namespace WebApp2_Magazin.Abstraction
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        int AddStorage(StorageDto storage);
    }
}
