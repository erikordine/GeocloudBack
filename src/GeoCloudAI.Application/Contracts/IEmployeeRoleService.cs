using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IEmployeeRoleService
    {
        Task<EmployeeRoleDto> Add(EmployeeRoleDto employeeRoleDto);
        Task<EmployeeRoleDto> Update(EmployeeRoleDto employeeRoleDto);
        Task<int> Delete(int employeeRoleId);

        Task<PageList<EmployeeRoleDto>> Get(PageParams pageParams);
        Task<PageList<EmployeeRoleDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<EmployeeRoleDto> GetById(int employeeRoleId);
    }
}