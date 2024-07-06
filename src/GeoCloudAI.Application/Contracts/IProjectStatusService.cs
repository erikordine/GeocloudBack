using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IProjectStatusService
    {
        Task<ProjectStatusDto> Add(ProjectStatusDto projectStatusDto);
        Task<ProjectStatusDto> Update(ProjectStatusDto projectStatusDto);
        Task<int>              Delete(int projectStatusId);

        Task<PageList<ProjectStatusDto>> Get(PageParams pageParams);
        Task<PageList<ProjectStatusDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<ProjectStatusDto> GetById(int projectStatusId);
    }
}