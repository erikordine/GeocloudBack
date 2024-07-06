using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IOreGeneticTypeRepository
    {
        Task<int> Add(OreGeneticType oreGeneticType);
        Task<int> Update(OreGeneticType oreGeneticType);
        Task<int> Delete(int id);

        Task<PageList<OreGeneticType>> Get(PageParams pageParams);
        Task<PageList<OreGeneticType>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<OreGeneticType>> GetByDepositType(int depositTypeId, PageParams pageParams);
        Task<OreGeneticType> GetById(int id);
    }
}