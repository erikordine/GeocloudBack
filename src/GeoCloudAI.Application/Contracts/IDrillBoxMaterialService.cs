using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxMaterialService
    {
        Task<DrillBoxMaterialDto> Add(DrillBoxMaterialDto drillBoxMaterialDto);
        Task<DrillBoxMaterialDto> Update(DrillBoxMaterialDto drillBoxMaterialDto);
        Task<int>                 Delete(int drillBoxMaterialId);

        Task<PageList<DrillBoxMaterialDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxMaterialDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxMaterialDto> GetById(int drillBoxMaterialId);
    }
}