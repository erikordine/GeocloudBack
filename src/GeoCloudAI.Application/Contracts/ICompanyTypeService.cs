using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface ICompanyTypeService
    {
        Task<CompanyTypeDto> Add(CompanyTypeDto companyTypeDto);
        Task<CompanyTypeDto> Update(CompanyTypeDto companyTypeDto);
        Task<int> Delete(int companyTypeId);

        Task<PageList<CompanyTypeDto>> Get(PageParams pageParams);
        Task<PageList<CompanyTypeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<CompanyTypeDto> GetById(int companyTypeId);
    }
}