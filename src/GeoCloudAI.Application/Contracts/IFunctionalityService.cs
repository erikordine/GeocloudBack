using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IFunctionalityService
    {
        Task<FunctionalityDto> Add(FunctionalityDto functionalityDto);
        Task<FunctionalityDto> Update(FunctionalityDto functionalityDto);
        Task<int>              Delete(int functionalityId);

        Task<PageList<FunctionalityDto>> Get(PageParams pageParams);
        Task<FunctionalityDto> GetById(int functionalityId);
    }
}