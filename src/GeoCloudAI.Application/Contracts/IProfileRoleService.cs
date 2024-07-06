using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    public interface IProfileRoleService
    {
        Task<ProfileRoleDto> Add(ProfileRoleDto profileRoleDto);
        Task<int> Delete(int profileRoleId);

        Task<PageList<ProfileRoleDto>> Get(PageParams pageParams);
        Task<PageList<ProfileRoleDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<ProfileRoleDto>> GetByProfile(int profileId, PageParams pageParams);
        Task<ProfileRoleDto> GetById(int id);
    }
}