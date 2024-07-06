using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxTypeRepository
    {
        Task<int> Add(DrillBoxType drillBoxType);
        Task<int> Update(DrillBoxType drillBoxType);
        Task<int> Delete(int id);

        Task<PageList<DrillBoxType>> Get(PageParams pageParams);
        Task<PageList<DrillBoxType>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxType> GetById(int id);
    }
}