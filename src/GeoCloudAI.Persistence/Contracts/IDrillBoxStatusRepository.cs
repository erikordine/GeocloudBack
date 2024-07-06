using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxStatusRepository
    {
        Task<int> Add(DrillBoxStatus drillBoxStatus);
        Task<int> Update(DrillBoxStatus drillBoxStatus);
        Task<int> Delete(int id);

        Task<PageList<DrillBoxStatus>> Get(PageParams pageParams);
        Task<PageList<DrillBoxStatus>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxStatus> GetById(int id);
    }
}