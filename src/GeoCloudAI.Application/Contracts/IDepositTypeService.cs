using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDepositTypeService
    {
        Task<DepositTypeDto> Add(DepositTypeDto depositTypeDto);
        Task<DepositTypeDto> Update(DepositTypeDto depositTypeDto);
        Task<int> Delete(int depositTypeId);

        Task<PageList<DepositTypeDto>> Get(PageParams pageParams);
        Task<PageList<DepositTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<DepositTypeDto> GetById(int depositTypeId);
    }
}