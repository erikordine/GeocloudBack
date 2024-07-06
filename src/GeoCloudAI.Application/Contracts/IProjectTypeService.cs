using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IProjectTypeService
    {
        Task<ProjectTypeDto> Add(ProjectTypeDto projectTypeDto);
        Task<ProjectTypeDto> Update(ProjectTypeDto projectTypeDto);
        Task<int>            Delete(int projectTypeId);

        Task<PageList<ProjectTypeDto>> Get(PageParams pageParams);
        Task<PageList<ProjectTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<ProjectTypeDto> GetById(int projectTypeId);
    }
}