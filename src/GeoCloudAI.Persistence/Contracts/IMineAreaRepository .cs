using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineAreaRepository
    {
        Task<int> Add(MineArea mineArea);
        Task<int> Update(MineArea mineArea);
        Task<int> Delete(int id);

        Task<PageList<MineArea>> Get(PageParams pageParams);
        Task<PageList<MineArea>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MineArea>> GetByRegion(int regionId, PageParams pageParams);
        Task<PageList<MineArea>> GetByDeposit(int depositId, PageParams pageParams);
        Task<PageList<MineArea>> GetByMine(int mineId, PageParams pageParams);
        Task<MineArea> GetById(int id); 
    }
}