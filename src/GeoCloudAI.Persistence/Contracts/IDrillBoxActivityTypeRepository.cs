using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxActivityTypeRepository
    {
        Task<int> Add(DrillBoxActivityType drillBoxActivityType);
        Task<int> Update(DrillBoxActivityType drillBoxActivityType);
        Task<int> Delete(int id);

        Task<PageList<DrillBoxActivityType>> Get(PageParams pageParams);
        Task<PageList<DrillBoxActivityType>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxActivityType> GetById(int id);
    }
}