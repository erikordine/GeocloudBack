using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IProjectTypeRepository
    {
        Task<int> Add(ProjectType projectType);
        Task<int> Update(ProjectType projectType);
        Task<int> Delete(int id);

        Task<PageList<ProjectType>> Get(PageParams pageParams);
        Task<PageList<ProjectType>> GetByAccount(int accountId, PageParams pageParams);
        Task<ProjectType> GetById(int id);
    }
}