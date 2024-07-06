using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class CoreShedService: ICoreShedService
    {
        private readonly ICoreShedRepository _coreShedRepository;

        private readonly IMapper _mapper;
        
        public CoreShedService(ICoreShedRepository coreShedRepository,
                           IMapper mapper)
        {
            _coreShedRepository = coreShedRepository;
            _mapper = mapper;
        }

        public async Task<CoreShedDto> Add(CoreShedDto coreShedDto) 
        {
            try
            {
                //Map Dto > Class
                var addCoreShed = _mapper.Map<Domain.Classes.CoreShed>(coreShedDto); 
                //Add CoreShed
                var resultCode = await _coreShedRepository.Add(addCoreShed); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New CoreShed
                var result = await _coreShedRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CoreShedDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<CoreShedDto> Update(CoreShedDto coreShedDto) 
        {
            try
            {
                //Check if exist CoreShed
                var existCoreShed = await _coreShedRepository.GetById(coreShedDto.Id);
                if (existCoreShed == null) return null;
                //Map Dto > Class
                var updateCoreShed = _mapper.Map<Domain.Classes.CoreShed>(coreShedDto);
                //Update CoreShed
                var resultCode = await _coreShedRepository.Update(updateCoreShed); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated CoreShed
                var result = await _coreShedRepository.GetById(updateCoreShed.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CoreShedDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int coreShedId) 
        {
            try
            {
                return await _coreShedRepository.Delete(coreShedId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<CoreShedDto>> Get(PageParams pageParams) 
        {
            try
            {
                var coreSheds = await _coreShedRepository.Get(pageParams);
                if (coreSheds == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CoreShedDto>>(coreSheds);
                result.TotalCount  = coreSheds.TotalCount;
                result.CurrentPage = coreSheds.CurrentPage;
                result.PageSize    = coreSheds.PageSize;
                result.TotalPages  = coreSheds.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<CoreShedDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var coreSheds = await _coreShedRepository.GetByAccount(accountId, pageParams);
                if (coreSheds == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CoreShedDto>>(coreSheds);
                result.TotalCount  = coreSheds.TotalCount;
                result.CurrentPage = coreSheds.CurrentPage;
                result.PageSize    = coreSheds.PageSize;
                result.TotalPages  = coreSheds.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<CoreShedDto> GetById(int coreShedId) 
        {
            try
            {
                var coreShed = await _coreShedRepository.GetById(coreShedId);
                if (coreShed == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<CoreShedDto>(coreShed);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}