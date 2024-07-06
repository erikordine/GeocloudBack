using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillHoleRepository
    {
        Task<int> Add(DrillHole drillHole);
        Task<int> Update(DrillHole drillHole);
        Task<int> Delete(int id);

        Task<PageList<DrillHole>> Get(PageParams pageParams);
        Task<PageList<DrillHole>> GetByAccount (int accountId, PageParams pageParams);
        Task<PageList<DrillHole>> GetByRegion  (int regionId, bool direct, PageParams pageParams);
        Task<PageList<DrillHole>> GetByDeposit (int depositId, bool direct, PageParams pageParams);
        Task<PageList<DrillHole>> GetByMine    (int mineId, bool direct, PageParams pageParams);
        Task<PageList<DrillHole>> GetByMineArea(int mineAreaId, PageParams pageParams);
        Task<DrillHole> GetById(int id); 
    }
}