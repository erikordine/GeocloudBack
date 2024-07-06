using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxActivityTypeService
    {
        Task<DrillBoxActivityTypeDto> Add(DrillBoxActivityTypeDto drillBoxActivityTypeDto);
        Task<DrillBoxActivityTypeDto> Update(DrillBoxActivityTypeDto drillBoxActivityTypeDto);
        Task<int> Delete(int drillBoxActivityTypeId);

        Task<PageList<DrillBoxActivityTypeDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxActivityTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxActivityTypeDto> GetById(int drillBoxActivityTypeId);
    }
}