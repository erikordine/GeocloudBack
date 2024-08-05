using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxActivityRepository
    {
        Task<int> Add(DrillBoxActivity drillBoxActivity);
        Task<int> Update(DrillBoxActivity drillBoxActivity);
        Task<int> Delete(int id);

        Task<PageList<DrillBoxActivity>> Get(PageParams pageParams);
        Task<PageList<DrillBoxActivity>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<DrillBoxActivity>> GetByDrillBox(int drillBoxId, PageParams pageParams);
        Task<DrillBoxActivity> GetById(int id); 
    }
}