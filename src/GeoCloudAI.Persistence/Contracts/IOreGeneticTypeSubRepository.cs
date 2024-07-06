using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IOreGeneticTypeSubRepository
    {
        Task<int> Add(OreGeneticTypeSub oreGeneticTypeSub);
        Task<int> Update(OreGeneticTypeSub oreGeneticTypeSub);
        Task<int> Delete(int id);

        Task<PageList<OreGeneticTypeSub>> Get(PageParams pageParams);
        Task<PageList<OreGeneticTypeSub>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<OreGeneticTypeSub>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams);
        Task<OreGeneticTypeSub> GetById(int id);
    }
}