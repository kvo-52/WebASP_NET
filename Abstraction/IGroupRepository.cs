using WebApp1_Product.Models.DTO;

namespace WebApp1_Product.Abstraction
{
    public interface IGroupRepository
    {
        public int AddGroup(DTOGroup group);
        public string GetGroupsCSV();
        public string GetСacheStatCSV();

        public IEnumerable<DTOGroup> GetGroups();
    }
}
