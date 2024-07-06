using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IProjectService
    {
        Task<ProjectDto> Add(ProjectDto projectDto);
        Task<ProjectDto> Update(ProjectDto projectDto);
        Task<int>        Delete(int projectId);

        Task<PageList<ProjectDto>> Get(PageParams pageParams);
        Task<PageList<ProjectDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<ProjectDto> GetById(int projectId);
    }
}