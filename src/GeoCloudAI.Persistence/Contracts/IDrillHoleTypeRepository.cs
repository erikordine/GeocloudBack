using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillHoleTypeRepository
    {
        Task<int> Add(DrillHoleType drillHoleType);
        Task<int> Update(DrillHoleType drillHoleType);
        Task<int> Delete(int id);

        Task<PageList<DrillHoleType>> Get(PageParams pageParams);
        Task<PageList<DrillHoleType>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillHoleType> GetById(int id);
    }
}