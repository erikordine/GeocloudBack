using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class UnitService: IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        private readonly IMapper _mapper;
        
        public UnitService(IUnitRepository unitRepository,
                           IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<UnitDto> Add(UnitDto unitDto) 
        {
            try
            {
                //Map Dto > Class
                var addUnit = _mapper.Map<Unit>(unitDto); 
                //Add Unit
                var resultCode = await _unitRepository.Add(addUnit); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Unit
                var result = await _unitRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UnitDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<UnitDto> Update(UnitDto unitDto) 
        {
            try
            {
                //Check if exist Unit
                var existUnit = await _unitRepository.GetById(unitDto.Id);
                if (existUnit == null) return null;
                //Map Dto > Class
                var updateUnit = _mapper.Map<Unit>(unitDto);
                //Update Unit
                var resultCode = await _unitRepository.Update(updateUnit); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Unit
                var result = await _unitRepository.GetById(updateUnit.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<UnitDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int unitId) 
        {
            try
            {
                return await _unitRepository.Delete(unitId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<UnitDto>> Get(PageParams pageParams) 
        {
            try
            {
                var units = await _unitRepository.Get(pageParams);
                if (units == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<UnitDto>>(units);
                result.TotalCount  = units.TotalCount;
                result.CurrentPage = units.CurrentPage;
                result.PageSize    = units.PageSize;
                result.TotalPages  = units.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<UnitDto>> GetByUnitType(int unitTypeId, PageParams pageParams) 
        {
            try
            {
                var units = await _unitRepository.GetByUnitType(unitTypeId, pageParams);
                if (units == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<UnitDto>>(units);
                result.TotalCount  = units.TotalCount;
                result.CurrentPage = units.CurrentPage;
                result.PageSize    = units.PageSize;
                result.TotalPages  = units.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<UnitDto> GetById(int unitId) 
        {
            try
            {
                var unit = await _unitRepository.GetById(unitId);
                if (unit == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<UnitDto>(unit);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}