using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillBoxMaterialRepository
    {
        Task<int> Add(DrillBoxMaterial drillBoxMaterial);
        Task<int> Update(DrillBoxMaterial drillBoxMaterial);
        Task<int> Delete(int id);

        Task<PageList<DrillBoxMaterial>> Get(PageParams pageParams);
        Task<PageList<DrillBoxMaterial>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxMaterial> GetById(int id);
    }
}