using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> Add(EmployeeDto employeeDto);
        Task<EmployeeDto> Update(EmployeeDto employeeDto);
        Task<int>         Delete(int employeeId);

        Task<PageList<EmployeeDto>> Get(PageParams pageParams);
        Task<PageList<EmployeeDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<EmployeeDto>> GetByCompany(int companyId, PageParams pageParams);
        Task<EmployeeDto> GetById(int employeeId);
    }
}