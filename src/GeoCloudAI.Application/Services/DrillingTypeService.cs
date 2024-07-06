using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillingTypeService: IDrillingTypeService
    {
        private readonly IDrillingTypeRepository _drillingTypeRepository;

        private readonly IMapper _mapper;
        
        public DrillingTypeService(IDrillingTypeRepository drillingTypeRepository,
                           IMapper mapper)
        {
            _drillingTypeRepository = drillingTypeRepository;
            _mapper = mapper;
        }

        public async Task<DrillingTypeDto> Add(DrillingTypeDto drillingTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillingType = _mapper.Map<Domain.Classes.DrillingType>(drillingTypeDto); 
                //Add DrillingType
                var resultCode = await _drillingTypeRepository.Add(addDrillingType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillingType
                var result = await _drillingTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillingTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillingTypeDto> Update(DrillingTypeDto drillingTypeDto) 
        {
            try
            {
                //Check if exist DrillingType
                var existDrillingType = await _drillingTypeRepository.GetById(drillingTypeDto.Id);
                if (existDrillingType == null) return null;
                //Map Dto > Class
                var updateDrillingType = _mapper.Map<Domain.Classes.DrillingType>(drillingTypeDto);
                //Update DrillingType
                var resultCode = await _drillingTypeRepository.Update(updateDrillingType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillingType
                var result = await _drillingTypeRepository.GetById(updateDrillingType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillingTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillingTypeId) 
        {
            try
            {
                return await _drillingTypeRepository.Delete(drillingTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillingTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillingTypes = await _drillingTypeRepository.Get(pageParams);
                if (drillingTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillingTypeDto>>(drillingTypes);
                result.TotalCount  = drillingTypes.TotalCount;
                result.CurrentPage = drillingTypes.CurrentPage;
                result.PageSize    = drillingTypes.PageSize;
                result.TotalPages  = drillingTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillingTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillingTypes = await _drillingTypeRepository.GetByAccount(accountId, pageParams);
                if (drillingTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillingTypeDto>>(drillingTypes);
                result.TotalCount  = drillingTypes.TotalCount;
                result.CurrentPage = drillingTypes.CurrentPage;
                result.PageSize    = drillingTypes.PageSize;
                result.TotalPages  = drillingTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillingTypeDto> GetById(int drillingTypeId) 
        {
            try
            {
                var drillingType = await _drillingTypeRepository.GetById(drillingTypeId);
                if (drillingType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillingTypeDto>(drillingType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}