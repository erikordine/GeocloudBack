using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDrillingTypeService
    {
        Task<DrillingTypeDto> Add(DrillingTypeDto drillingTypeDto);
        Task<DrillingTypeDto> Update(DrillingTypeDto drillingTypeDto);
        Task<int> Delete(int drillingTypeId);

        Task<PageList<DrillingTypeDto>> Get(PageParams pageParams);
        Task<PageList<DrillingTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DrillingTypeDto> GetById(int drillingTypeId);
    }
}