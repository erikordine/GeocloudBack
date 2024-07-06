using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillHoleTypeService: IDrillHoleTypeService
    {
        private readonly IDrillHoleTypeRepository _drillHoleTypeRepository;

        private readonly IMapper _mapper;
        
        public DrillHoleTypeService(IDrillHoleTypeRepository drillHoleTypeRepository,
                           IMapper mapper)
        {
            _drillHoleTypeRepository = drillHoleTypeRepository;
            _mapper = mapper;
        }

        public async Task<DrillHoleTypeDto> Add(DrillHoleTypeDto drillHoleTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillHoleType = _mapper.Map<Domain.Classes.DrillHoleType>(drillHoleTypeDto); 
                //Add DrillHoleType
                var resultCode = await _drillHoleTypeRepository.Add(addDrillHoleType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillHoleType
                var result = await _drillHoleTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillHoleTypeDto> Update(DrillHoleTypeDto drillHoleTypeDto) 
        {
            try
            {
                //Check if exist DrillHoleType
                var existDrillHoleType = await _drillHoleTypeRepository.GetById(drillHoleTypeDto.Id);
                if (existDrillHoleType == null) return null;
                //Map Dto > Class
                var updateDrillHoleType = _mapper.Map<Domain.Classes.DrillHoleType>(drillHoleTypeDto);
                //Update DrillHoleType
                var resultCode = await _drillHoleTypeRepository.Update(updateDrillHoleType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillHoleType
                var result = await _drillHoleTypeRepository.GetById(updateDrillHoleType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillHoleTypeId) 
        {
            try
            {
                return await _drillHoleTypeRepository.Delete(drillHoleTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillHoleTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillHoleTypes = await _drillHoleTypeRepository.Get(pageParams);
                if (drillHoleTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleTypeDto>>(drillHoleTypes);
                result.TotalCount  = drillHoleTypes.TotalCount;
                result.CurrentPage = drillHoleTypes.CurrentPage;
                result.PageSize    = drillHoleTypes.PageSize;
                result.TotalPages  = drillHoleTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillHoleTypes = await _drillHoleTypeRepository.GetByAccount(accountId, pageParams);
                if (drillHoleTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleTypeDto>>(drillHoleTypes);
                result.TotalCount  = drillHoleTypes.TotalCount;
                result.CurrentPage = drillHoleTypes.CurrentPage;
                result.PageSize    = drillHoleTypes.PageSize;
                result.TotalPages  = drillHoleTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillHoleTypeDto> GetById(int drillHoleTypeId) 
        {
            try
            {
                var drillHoleType = await _drillHoleTypeRepository.GetById(drillHoleTypeId);
                if (drillHoleType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillHoleTypeDto>(drillHoleType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}