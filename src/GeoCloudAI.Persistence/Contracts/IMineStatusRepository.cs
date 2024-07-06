using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineStatusRepository
    {
        Task<int> Add(MineStatus mineStatus);
        Task<int> Update(MineStatus mineStatus);
        Task<int> Delete(int id);

        Task<PageList<MineStatus>> Get(PageParams pageParams);
        Task<PageList<MineStatus>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineStatus> GetById(int id);
    }
}