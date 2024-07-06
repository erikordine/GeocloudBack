using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class UnitTypeService: IUnitTypeService
    {
        private readonly IUnitTypeRepository _unitTypeRepository;

        private readonly IMapper _mapper;
        
        public UnitTypeService(IUnitTypeRepository unitTypeRepository,
                           IMapper mapper)
        {
            _unitTypeRepository = unitTypeRepository;
            _mapper = mapper;
        }

        public async Task<UnitTypeDto> Add(UnitTypeDto unitTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addUnitType = _mapper.Map<UnitType>(unitTypeDto); 
                //Add UnitType
                var resultCode = await _unitTypeRepository.Add(addUnitType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New UnitType
                var result = await _unitTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UnitTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<UnitTypeDto> Update(UnitTypeDto unitTypeDto) 
        {
            try
            {
                //Check if exist UnitType
                var existUnitType = await _unitTypeRepository.GetById(unitTypeDto.Id);
                if (existUnitType == null) return null;
                //Map Dto > Class
                var updateUnitType = _mapper.Map<UnitType>(unitTypeDto);
                //Update UnitType
                var resultCode = await _unitTypeRepository.Update(updateUnitType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated UnitType
                var result = await _unitTypeRepository.GetById(updateUnitType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UnitTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int unitTypeId) 
        {
            try
            {
                return await _unitTypeRepository.Delete(unitTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<UnitTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var unitTypes = await _unitTypeRepository.Get(pageParams);
                if (unitTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<UnitTypeDto>>(unitTypes);
                result.TotalCount  = unitTypes.TotalCount;
                result.CurrentPage = unitTypes.CurrentPage;
                result.PageSize    = unitTypes.PageSize;
                result.TotalPages  = unitTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<UnitTypeDto> GetById(int unitTypeId) 
        {
            try
            {
                var unitType = await _unitTypeRepository.GetById(unitTypeId);
                if (unitType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<UnitTypeDto>(unitType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}