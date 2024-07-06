using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxTypeService
    {
        Task<DrillBoxTypeDto> Add(DrillBoxTypeDto drillBoxTypeDto);
        Task<DrillBoxTypeDto> Update(DrillBoxTypeDto drillBoxTypeDto);
        Task<int>             Delete(int drillBoxTypeId);

        Task<PageList<DrillBoxTypeDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxTypeDto> GetById(int drillBoxTypeId);
    }
}