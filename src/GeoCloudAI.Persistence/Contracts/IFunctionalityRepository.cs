using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IFunctionalityRepository
    {
        Task<int> Add(Functionality functionality);
        Task<int> Update(Functionality functionality);
        Task<int> Delete(int id);

        Task<PageList<Functionality>> Get(PageParams pageParams);
        Task<Functionality> GetById(int id);
    }
}