using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class LithologyService: ILithologyService
    {
        private readonly ILithologyRepository _lithologyRepository;

        private readonly IMapper _mapper;
        
        public LithologyService(ILithologyRepository lithologyRepository,
                           IMapper mapper)
        {
            _lithologyRepository = lithologyRepository;
            _mapper = mapper;
        }

        public async Task<LithologyDto> Add(LithologyDto lithologyDto) 
        {
            try
            {
                //Map Dto > Class
                var addLithology = _mapper.Map<Domain.Classes.Lithology>(lithologyDto); 
                //Add Lithology
                var resultCode = await _lithologyRepository.Add(addLithology); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Lithology
                var result = await _lithologyRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<LithologyDto> Update(LithologyDto lithologyDto) 
        {
            try
            {
                //Check if exist Lithology
                var existLithology = await _lithologyRepository.GetById(lithologyDto.Id);
                if (existLithology == null) return null;
                //Map Dto > Class
                var updateLithology = _mapper.Map<Domain.Classes.Lithology>(lithologyDto);
                //Update Lithology
                var resultCode = await _lithologyRepository.Update(updateLithology); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Lithology
                var result = await _lithologyRepository.GetById(updateLithology.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int lithologyId) 
        {
            try
            {
                return await _lithologyRepository.Delete(lithologyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<LithologyDto>> Get(PageParams pageParams) 
        {
            try
            {
                var lithologys = await _lithologyRepository.Get(pageParams);
                if (lithologys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyDto>>(lithologys);
                result.TotalCount  = lithologys.TotalCount;
                result.CurrentPage = lithologys.CurrentPage;
                result.PageSize    = lithologys.PageSize;
                result.TotalPages  = lithologys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var lithologys = await _lithologyRepository.GetByAccount(accountId, pageParams);
                if (lithologys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyDto>>(lithologys);
                result.TotalCount  = lithologys.TotalCount;
                result.CurrentPage = lithologys.CurrentPage;
                result.PageSize    = lithologys.PageSize;
                result.TotalPages  = lithologys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyDto>> GetByLithologyGroupSub(int groupSubId, PageParams pageParams) 
        {
            try
            {
                var lithologys = await _lithologyRepository.GetByLithologyGroupSub(groupSubId, pageParams);
                if (lithologys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyDto>>(lithologys);
                result.TotalCount  = lithologys.TotalCount;
                result.CurrentPage = lithologys.CurrentPage;
                result.PageSize    = lithologys.PageSize;
                result.TotalPages  = lithologys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<LithologyDto> GetById(int lithologyId) 
        {
            try
            {
                var lithology = await _lithologyRepository.GetById(lithologyId);
                if (lithology == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<LithologyDto>(lithology);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}