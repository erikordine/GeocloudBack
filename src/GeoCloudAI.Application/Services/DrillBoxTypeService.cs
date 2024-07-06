using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxTypeService: IDrillBoxTypeService
    {
        private readonly IDrillBoxTypeRepository _drillBoxTypeRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxTypeService(IDrillBoxTypeRepository drillBoxTypeRepository,
                           IMapper mapper)
        {
            _drillBoxTypeRepository = drillBoxTypeRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxTypeDto> Add(DrillBoxTypeDto drillBoxTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBoxType = _mapper.Map<DrillBoxType>(drillBoxTypeDto); 
                //Add DrillBoxType
                var resultCode = await _drillBoxTypeRepository.Add(addDrillBoxType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBoxType
                var result = await _drillBoxTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxTypeDto> Update(DrillBoxTypeDto drillBoxTypeDto) 
        {
            try
            {
                //Check if exist DrillBoxType
                var existDrillBoxType = await _drillBoxTypeRepository.GetById(drillBoxTypeDto.Id);
                if (existDrillBoxType == null) return null;
                //Map Dto > Class
                var updateDrillBoxType = _mapper.Map<DrillBoxType>(drillBoxTypeDto);
                //Update DrillBoxType
                var resultCode = await _drillBoxTypeRepository.Update(updateDrillBoxType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBoxType
                var result = await _drillBoxTypeRepository.GetById(updateDrillBoxType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxTypeId) 
        {
            try
            {
                return await _drillBoxTypeRepository.Delete(drillBoxTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxTypes = await _drillBoxTypeRepository.Get(pageParams);
                if (drillBoxTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxTypeDto>>(drillBoxTypes);
                result.TotalCount  = drillBoxTypes.TotalCount;
                result.CurrentPage = drillBoxTypes.CurrentPage;
                result.PageSize    = drillBoxTypes.PageSize;
                result.TotalPages  = drillBoxTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxTypes = await _drillBoxTypeRepository.GetByAccount(accountId, pageParams);
                if (drillBoxTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxTypeDto>>(drillBoxTypes);
                result.TotalCount  = drillBoxTypes.TotalCount;
                result.CurrentPage = drillBoxTypes.CurrentPage;
                result.PageSize    = drillBoxTypes.PageSize;
                result.TotalPages  = drillBoxTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxTypeDto> GetById(int drillBoxTypeId) 
        {
            try
            {
                var drillBoxType = await _drillBoxTypeRepository.GetById(drillBoxTypeId);
                if (drillBoxType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxTypeDto>(drillBoxType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}