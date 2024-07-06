using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxRepository
    {
        Task<int> Add(DrillBox drillBox);
        Task<int> Update(DrillBox drillBox);
        Task<int> Delete(int id);

        Task<PageList<DrillBox>> Get(PageParams pageParams);
        Task<PageList<DrillBox>> GetByAccount  (int accountId, PageParams pageParams);
        Task<PageList<DrillBox>> GetByRegion   (int regionId, PageParams pageParams);
        Task<PageList<DrillBox>> GetByDeposit  (int depositId, PageParams pageParams);
        Task<PageList<DrillBox>> GetByMine     (int mineId, PageParams pageParams);
        Task<PageList<DrillBox>> GetByMineArea (int mineAreaId, PageParams pageParams);
        Task<PageList<DrillBox>> GetByDrillHole(int drillHoleId, PageParams pageParams);
        Task<DrillBox> GetById(int id); 
    }
}