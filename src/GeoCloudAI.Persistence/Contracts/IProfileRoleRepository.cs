using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IProfileRoleRepository
    {
        Task<int> Add(ProfileRole profileRole);
        Task<int> Delete(int id);

        Task<PageList<ProfileRole>> Get(PageParams pageParams);
        Task<PageList<ProfileRole>> GetByAccount(int accountId, PageParams pageParams);
        Task<PageList<ProfileRole>> GetByProfile(int profileId, PageParams pageParams);
        Task<ProfileRole> GetById(int id);

    }
}