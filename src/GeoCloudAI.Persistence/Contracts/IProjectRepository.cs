using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IProjectRepository
    {
        Task<int> Add(Project project);
        Task<int> Update(Project project);
        Task<int> Delete(int id);

        Task<PageList<Project>> Get(PageParams pageParams);
        Task<PageList<Project>> GetByAccount(int accountId, PageParams pageParams);
        Task<Project> GetById(int id); 
    }
}