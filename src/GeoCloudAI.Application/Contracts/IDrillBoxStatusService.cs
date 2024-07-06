using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillBoxStatusService
    {
        Task<DrillBoxStatusDto> Add(DrillBoxStatusDto drillBoxStatusDto);
        Task<DrillBoxStatusDto> Update(DrillBoxStatusDto drillBoxStatusDto);
        Task<int>               Delete(int drillBoxStatusId);

        Task<PageList<DrillBoxStatusDto>> Get(PageParams pageParams);
        Task<PageList<DrillBoxStatusDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillBoxStatusDto> GetById(int drillBoxStatusId);
    }
}