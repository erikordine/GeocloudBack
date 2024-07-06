using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyDto> Add(CompanyDto companyDto);
        Task<CompanyDto> Update(CompanyDto companyDto);
        Task<int>        Delete(int companyId);

        Task<PageList<CompanyDto>> Get(PageParams pageParams);
        Task<PageList<CompanyDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<CompanyDto> GetById(int companyId);
    }
}