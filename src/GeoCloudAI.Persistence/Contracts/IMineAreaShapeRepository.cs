using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IMineAreaShapeRepository
    {
        Task<int> Add(MineAreaShape mineAreaShape);
        Task<int> Update(MineAreaShape mineAreaShape);
        Task<int> Delete(int id);

        Task<PageList<MineAreaShape>> Get(PageParams pageParams);
        Task<PageList<MineAreaShape>> GetByAccount(int accountId, PageParams pageParams);
        Task<MineAreaShape> GetById(int id);
    }
}