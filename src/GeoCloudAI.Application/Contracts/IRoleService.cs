using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IRoleService
    {
        Task<RoleDto> Add(RoleDto roleDto);
        Task<RoleDto> Update(RoleDto roleDto);
        Task<int>     Delete(int roleId);

        Task<PageList<RoleDto>> Get(PageParams pageParams);
        Task<PageList<RoleDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<RoleDto> GetById(int roleId);
    }
}