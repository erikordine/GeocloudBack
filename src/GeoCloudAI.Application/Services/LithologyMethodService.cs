using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class LithologyMethodService: ILithologyMethodService
    {
        private readonly ILithologyMethodRepository _lithologyMethodRepository;

        private readonly IMapper _mapper;
        
        public LithologyMethodService(ILithologyMethodRepository lithologyMethodRepository,
                           IMapper mapper)
        {
            _lithologyMethodRepository = lithologyMethodRepository;
            _mapper = mapper;
        }

        public async Task<LithologyMethodDto> Add(LithologyMethodDto lithologyMethodDto) 
        {
            try
            {
                //Map Dto > Class
                var addLithologyMethod = _mapper.Map<Domain.Classes.LithologyMethod>(lithologyMethodDto); 
                //Add LithologyMethod
                var resultCode = await _lithologyMethodRepository.Add(addLithologyMethod); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New LithologyMethod
                var result = await _lithologyMethodRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyMethodDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<LithologyMethodDto> Update(LithologyMethodDto lithologyMethodDto) 
        {
            try
            {
                //Check if exist LithologyMethod
                var existLithologyMethod = await _lithologyMethodRepository.GetById(lithologyMethodDto.Id);
                if (existLithologyMethod == null) return null;
                //Map Dto > Class
                var updateLithologyMethod = _mapper.Map<Domain.Classes.LithologyMethod>(lithologyMethodDto);
                //Update LithologyMethod
                var resultCode = await _lithologyMethodRepository.Update(updateLithologyMethod); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated LithologyMethod
                var result = await _lithologyMethodRepository.GetById(updateLithologyMethod.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyMethodDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int lithologyMethodId) 
        {
            try
            {
                return await _lithologyMethodRepository.Delete(lithologyMethodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<LithologyMethodDto>> Get(PageParams pageParams) 
        {
            try
            {
                var lithologyMethods = await _lithologyMethodRepository.Get(pageParams);
                if (lithologyMethods == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyMethodDto>>(lithologyMethods);
                result.TotalCount  = lithologyMethods.TotalCount;
                result.CurrentPage = lithologyMethods.CurrentPage;
                result.PageSize    = lithologyMethods.PageSize;
                result.TotalPages  = lithologyMethods.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyMethodDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var lithologyMethods = await _lithologyMethodRepository.GetByAccount(accountId, pageParams);
                if (lithologyMethods == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyMethodDto>>(lithologyMethods);
                result.TotalCount  = lithologyMethods.TotalCount;
                result.CurrentPage = lithologyMethods.CurrentPage;
                result.PageSize    = lithologyMethods.PageSize;
                result.TotalPages  = lithologyMethods.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<LithologyMethodDto> GetById(int lithologyMethodId) 
        {
            try
            {
                var lithologyMethod = await _lithologyMethodRepository.GetById(lithologyMethodId);
                if (lithologyMethod == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<LithologyMethodDto>(lithologyMethod);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}