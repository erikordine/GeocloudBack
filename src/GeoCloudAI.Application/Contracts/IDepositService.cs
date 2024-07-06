using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IDepositService
    {
        Task<DepositDto> Add(DepositDto depositDto);
        Task<DepositDto> Update(DepositDto depositDto);
        Task<int>        Delete(int depositId);

        Task<PageList<DepositDto>> Get(PageParams pageParams);
        Task<PageList<DepositDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<DepositDto>> GetByRegion(int regionId, PageParams pageParams);
        Task<DepositDto> GetById(int depositId);
    }
}