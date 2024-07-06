using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IOreGeneticTypeSubService
    {
        Task<OreGeneticTypeSubDto> Add(OreGeneticTypeSubDto oreGeneticTypeSubDto);
        Task<OreGeneticTypeSubDto> Update(OreGeneticTypeSubDto oreGeneticTypeSubDto);
        Task<int> Delete(int oreGeneticTypeSubId);

        Task<PageList<OreGeneticTypeSubDto>> Get(PageParams pageParams);
        Task<PageList<OreGeneticTypeSubDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<OreGeneticTypeSubDto>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams);
        Task<OreGeneticTypeSubDto> GetById(int oreGeneticTypeSubId);
    }
}