using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillHoleRunService
    {
        Task<DrillHoleRunDto> Add(DrillHoleRunDto drillHoleRunDto);
        Task<DrillHoleRunDto> Update(DrillHoleRunDto drillHoleRunDto);
        Task<int>             Delete(int drillHoleRunId);

        Task<PageList<DrillHoleRunDto>> Get(PageParams pageParams);
        Task<PageList<DrillHoleRunDto>> GetByDrillHole(int drillHoleId, PageParams pageParams);
        Task<DrillHoleRunDto> GetById(int drillHoleRunId);
    }
}