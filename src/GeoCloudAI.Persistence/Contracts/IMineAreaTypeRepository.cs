using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineAreaTypeRepository
    {
        Task<int> Add(MineAreaType mineAreaType);
        Task<int> Update(MineAreaType mineAreaType);
        Task<int> Delete(int id);

        Task<PageList<MineAreaType>> Get(PageParams pageParams);
        Task<PageList<MineAreaType>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaType> GetById(int id);
    }
}