using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class FunctionalityTypeService: IFunctionalityTypeService
    {
        private readonly IFunctionalityTypeRepository _functionalityTypeRepository;

        private readonly IMapper _mapper;
        
        public FunctionalityTypeService(IFunctionalityTypeRepository functionalityTypeRepository,
                           IMapper mapper)
        {
            _functionalityTypeRepository = functionalityTypeRepository;
            _mapper = mapper;
        }

        public async Task<FunctionalityTypeDto> Add(FunctionalityTypeDto functionalityTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addFunctionalityType = _mapper.Map<FunctionalityType>(functionalityTypeDto); 
                //Add FunctionalityType
                var resultCode = await _functionalityTypeRepository.Add(addFunctionalityType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New FunctionalityType
                var result = await _functionalityTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<FunctionalityTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<FunctionalityTypeDto> Update(FunctionalityTypeDto functionalityTypeDto) 
        {
            try
            {
                //Check if exist FunctionalityType
                var existFunctionalityType = await _functionalityTypeRepository.GetById(functionalityTypeDto.Id);
                if (existFunctionalityType == null) return null;
                //Map Dto > Class
                var updateFunctionalityType = _mapper.Map<FunctionalityType>(functionalityTypeDto);
                //Update FunctionalityType
                var resultCode = await _functionalityTypeRepository.Update(updateFunctionalityType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated FunctionalityType
                var result = await _functionalityTypeRepository.GetById(updateFunctionalityType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<FunctionalityTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int functionalityTypeId) 
        {
            try
            {
                return await _functionalityTypeRepository.Delete(functionalityTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<FunctionalityTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var functionalityTypes = await _functionalityTypeRepository.Get(pageParams);
                if (functionalityTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<FunctionalityTypeDto>>(functionalityTypes);
                result.TotalCount  = functionalityTypes.TotalCount;
                result.CurrentPage = functionalityTypes.CurrentPage;
                result.PageSize    = functionalityTypes.PageSize;
                result.TotalPages  = functionalityTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<FunctionalityTypeDto> GetById(int functionalityTypeId) 
        {
            try
            {
                var functionalityType = await _functionalityTypeRepository.GetById(functionalityTypeId);
                if (functionalityType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<FunctionalityTypeDto>(functionalityType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}