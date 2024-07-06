using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IFunctionalityTypeRepository
    {
        Task<int> Add(FunctionalityType functionalityType);
        Task<int> Update(FunctionalityType functionalityType);
        Task<int> Delete(int id);

        Task<PageList<FunctionalityType>> Get(PageParams pageParams);
        Task<FunctionalityType> GetById(int id);
    }
}