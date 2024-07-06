using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineAreaStatusRepository
    {
        Task<int> Add(MineAreaStatus mineAreaStatus);
        Task<int> Update(MineAreaStatus mineAreaStatus);
        Task<int> Delete(int id);

        Task<PageList<MineAreaStatus>> Get(PageParams pageParams);
        Task<PageList<MineAreaStatus>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaStatus> GetById(int id);
    }
}