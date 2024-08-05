using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxActivityService
    {
        Task<DrillBoxActivityDto> Add(DrillBoxActivityDto drillBoxActivityDto);
        Task<DrillBoxActivityDto> Update(DrillBoxActivityDto drillBoxActivityDto);
        Task<int> Delete(int drillBoxActivityId);

        Task<PageList<DrillBoxActivityDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxActivityDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<DrillBoxActivityDto>> GetByDrillBox(int drillBoxId, PageParams pageParams);
        Task<DrillBoxActivityDto> GetById(int drillBoxActivityId);
    }
}