using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMetalGroupRepository
    {
        Task<int> Add(MetalGroup metalGroup);
        Task<int> Update(MetalGroup metalGroup);
        Task<int> Delete(int id);

        Task<PageList<MetalGroup>> Get(PageParams pageParams);
        Task<PageList<MetalGroup>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<MetalGroup>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams);
        Task<MetalGroup> GetById(int id);
    }
}