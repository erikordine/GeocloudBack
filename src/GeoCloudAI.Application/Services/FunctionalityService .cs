using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class FunctionalityService: IFunctionalityService
    {
        private readonly IFunctionalityRepository _functionalityRepository;

        private readonly IMapper _mapper;
        
        public FunctionalityService(IFunctionalityRepository functionalityRepository,
                                     IMapper mapper)
        {
            _functionalityRepository = functionalityRepository;
            _mapper = mapper;
        }

        public async Task<FunctionalityDto> Add(FunctionalityDto functionalityDto) 
        {
            try
            {
                //Map Dto > Class
                var addFunctionality = _mapper.Map<Domain.Classes.Functionality>(functionalityDto); 
                //Add Functionality
                var resultCode = await _functionalityRepository.Add(addFunctionality); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Functionality
                var result = await _functionalityRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<FunctionalityDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<FunctionalityDto> Update(FunctionalityDto functionalityDto) 
        {
            try
            {
                //Check if exist Functionality
                var existFunctionality = await _functionalityRepository.GetById(functionalityDto.Id);
                if (existFunctionality == null) return null;
                //Map Dto > Class
                var updateFunctionality = _mapper.Map<Domain.Classes.Functionality>(functionalityDto);
                //Update Functionality
                var resultCode = await _functionalityRepository.Update(updateFunctionality); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Functionality
                var result = await _functionalityRepository.GetById(updateFunctionality.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<FunctionalityDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int functionalityId) 
        {
            try
            {
                return await _functionalityRepository.Delete(functionalityId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<FunctionalityDto>> Get(PageParams pageParams) 
        {
            try
            {
                var functionalitys = await _functionalityRepository.Get(pageParams);
                if (functionalitys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<FunctionalityDto>>(functionalitys);
                result.TotalCount  = functionalitys.TotalCount;
                result.CurrentPage = functionalitys.CurrentPage;
                result.PageSize    = functionalitys.PageSize;
                result.TotalPages  = functionalitys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<FunctionalityDto> GetById(int functionalityId) 
        {
            try
            {
                var functionality = await _functionalityRepository.GetById(functionalityId);
                if (functionality == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<FunctionalityDto>(functionality);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}