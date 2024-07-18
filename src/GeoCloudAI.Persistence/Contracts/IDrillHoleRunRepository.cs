using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IDrillHoleRunRepository
    {
        Task<int> Add(DrillHoleRun drillHoleRun);
        Task<int> Update(DrillHoleRun drillHoleRun);
        Task<int> Delete(int id);

        Task<PageList<DrillHoleRun>> Get(PageParams pageParams);
        Task<PageList<DrillHoleRun>> GetByDrillHole(int drillHoleId, PageParams pageParams);
        Task<DrillHoleRun> GetById(int id); 
    }
}