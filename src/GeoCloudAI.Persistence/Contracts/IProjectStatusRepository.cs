using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IProjectStatusRepository
    {
        Task<int> Add(ProjectStatus projectStatus);
        Task<int> Update(ProjectStatus projectStatus);
        Task<int> Delete(int id);

        Task<PageList<ProjectStatus>> Get(PageParams pageParams);
        Task<PageList<ProjectStatus>> GetByAccount(int accountId, PageParams pageParams);
        Task<ProjectStatus> GetById(int id);
    }
}