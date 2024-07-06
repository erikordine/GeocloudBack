using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxActivityTypeService: IDrillBoxActivityTypeService
    {
        private readonly IDrillBoxActivityTypeRepository _drillBoxActivityTypeRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxActivityTypeService(IDrillBoxActivityTypeRepository drillBoxActivityTypeRepository,
                           IMapper mapper)
        {
            _drillBoxActivityTypeRepository = drillBoxActivityTypeRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxActivityTypeDto> Add(DrillBoxActivityTypeDto drillBoxActivityTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBoxActivityType = _mapper.Map<Domain.Classes.DrillBoxActivityType>(drillBoxActivityTypeDto); 
                //Add DrillBoxActivityType
                var resultCode = await _drillBoxActivityTypeRepository.Add(addDrillBoxActivityType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBoxActivityType
                var result = await _drillBoxActivityTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxActivityTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxActivityTypeDto> Update(DrillBoxActivityTypeDto drillBoxActivityTypeDto) 
        {
            try
            {
                //Check if exist DrillBoxActivityType
                var existDrillBoxActivityType = await _drillBoxActivityTypeRepository.GetById(drillBoxActivityTypeDto.Id);
                if (existDrillBoxActivityType == null) return null;
                //Map Dto > Class
                var updateDrillBoxActivityType = _mapper.Map<Domain.Classes.DrillBoxActivityType>(drillBoxActivityTypeDto);
                //Update DrillBoxActivityType
                var resultCode = await _drillBoxActivityTypeRepository.Update(updateDrillBoxActivityType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBoxActivityType
                var result = await _drillBoxActivityTypeRepository.GetById(updateDrillBoxActivityType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxActivityTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxActivityTypeId) 
        {
            try
            {
                return await _drillBoxActivityTypeRepository.Delete(drillBoxActivityTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxActivityTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxActivityTypes = await _drillBoxActivityTypeRepository.Get(pageParams);
                if (drillBoxActivityTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxActivityTypeDto>>(drillBoxActivityTypes);
                result.TotalCount  = drillBoxActivityTypes.TotalCount;
                result.CurrentPage = drillBoxActivityTypes.CurrentPage;
                result.PageSize    = drillBoxActivityTypes.PageSize;
                result.TotalPages  = drillBoxActivityTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxActivityTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxActivityTypes = await _drillBoxActivityTypeRepository.GetByAccount(accountId, pageParams);
                if (drillBoxActivityTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxActivityTypeDto>>(drillBoxActivityTypes);
                result.TotalCount  = drillBoxActivityTypes.TotalCount;
                result.CurrentPage = drillBoxActivityTypes.CurrentPage;
                result.PageSize    = drillBoxActivityTypes.PageSize;
                result.TotalPages  = drillBoxActivityTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxActivityTypeDto> GetById(int drillBoxActivityTypeId) 
        {
            try
            {
                var drillBoxActivityType = await _drillBoxActivityTypeRepository.GetById(drillBoxActivityTypeId);
                if (drillBoxActivityType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxActivityTypeDto>(drillBoxActivityType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}