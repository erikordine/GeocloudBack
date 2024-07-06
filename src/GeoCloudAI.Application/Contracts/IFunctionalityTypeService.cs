using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IFunctionalityTypeService
    {
        Task<FunctionalityTypeDto> Add(FunctionalityTypeDto functionalityTypeDto);
        Task<FunctionalityTypeDto> Update(FunctionalityTypeDto functionalityTypeDto);
        Task<int>                  Delete(int functionalityTypeId);

        Task<PageList<FunctionalityTypeDto>> Get(PageParams pageParams);
        Task<FunctionalityTypeDto> GetById(int functionalityTypeId);
    }
}