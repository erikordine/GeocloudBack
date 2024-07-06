using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineSizeRepository
    {
        Task<int> Add(MineSize mineSize);
        Task<int> Update(MineSize mineSize);
        Task<int> Delete(int id);

        Task<PageList<MineSize>> Get(PageParams pageParams);
        Task<PageList<MineSize>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineSize> GetById(int id);
    }
}