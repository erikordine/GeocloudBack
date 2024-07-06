using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IOreGeneticTypeService
    {
        Task<OreGeneticTypeDto> Add(OreGeneticTypeDto oreGeneticTypeDto);
        Task<OreGeneticTypeDto> Update(OreGeneticTypeDto oreGeneticTypeDto);
        Task<int>               Delete(int oreGeneticTypeId);

        Task<PageList<OreGeneticTypeDto>> Get(PageParams pageParams);
        Task<PageList<OreGeneticTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<OreGeneticTypeDto>> GetByDepositType(int depositTypeId, PageParams pageParams);
        Task<OreGeneticTypeDto> GetById(int oreGeneticTypeId);
    }
}