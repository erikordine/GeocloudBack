using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class LithologyGroupSubService: ILithologyGroupSubService
    {
        private readonly ILithologyGroupSubRepository _lithologyGroupSubRepository;

        private readonly IMapper _mapper;
        
        public LithologyGroupSubService(ILithologyGroupSubRepository lithologyGroupSubRepository,
                                        IMapper mapper)
        {
            _lithologyGroupSubRepository = lithologyGroupSubRepository;
            _mapper = mapper;
        }

        public async Task<LithologyGroupSubDto> Add(LithologyGroupSubDto lithologyGroupSubDto) 
        {
            try
            {
                //Map Dto > Class
                var addLithologyGroupSub = _mapper.Map<Domain.Classes.LithologyGroupSub>(lithologyGroupSubDto); 
                //Add LithologyGroupSub
                var resultCode = await _lithologyGroupSubRepository.Add(addLithologyGroupSub); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New LithologyGroupSub
                var result = await _lithologyGroupSubRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyGroupSubDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<LithologyGroupSubDto> Update(LithologyGroupSubDto lithologyGroupSubDto) 
        {
            try
            {
                //Check if exist LithologyGroupSub
                var existLithologyGroupSub = await _lithologyGroupSubRepository.GetById(lithologyGroupSubDto.Id);
                if (existLithologyGroupSub == null) return null;
                //Map Dto > Class
                var updateLithologyGroupSub = _mapper.Map<Domain.Classes.LithologyGroupSub>(lithologyGroupSubDto);
                //Update LithologyGroupSub
                var resultCode = await _lithologyGroupSubRepository.Update(updateLithologyGroupSub); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated LithologyGroupSub
                var result = await _lithologyGroupSubRepository.GetById(updateLithologyGroupSub.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyGroupSubDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int lithologyGroupSubId) 
        {
            try
            {
                return await _lithologyGroupSubRepository.Delete(lithologyGroupSubId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<LithologyGroupSubDto>> Get(PageParams pageParams) 
        {
            try
            {
                var lithologyGroupSubs = await _lithologyGroupSubRepository.Get(pageParams);
                if (lithologyGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyGroupSubDto>>(lithologyGroupSubs);
                result.TotalCount  = lithologyGroupSubs.TotalCount;
                result.CurrentPage = lithologyGroupSubs.CurrentPage;
                result.PageSize    = lithologyGroupSubs.PageSize;
                result.TotalPages  = lithologyGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyGroupSubDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var lithologyGroupSubs = await _lithologyGroupSubRepository.GetByAccount(accountId, pageParams);
                if (lithologyGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyGroupSubDto>>(lithologyGroupSubs);
                result.TotalCount  = lithologyGroupSubs.TotalCount;
                result.CurrentPage = lithologyGroupSubs.CurrentPage;
                result.PageSize    = lithologyGroupSubs.PageSize;
                result.TotalPages  = lithologyGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyGroupSubDto>> GetByLithologyGroup(int groupId, PageParams pageParams) 
        {
            try
            {
                var lithologyGroupSubs = await _lithologyGroupSubRepository.GetByLithologyGroup(groupId, pageParams);
                if (lithologyGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyGroupSubDto>>(lithologyGroupSubs);
                result.TotalCount  = lithologyGroupSubs.TotalCount;
                result.CurrentPage = lithologyGroupSubs.CurrentPage;
                result.PageSize    = lithologyGroupSubs.PageSize;
                result.TotalPages  = lithologyGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<LithologyGroupSubDto> GetById(int lithologyGroupSubId) 
        {
            try
            {
                var lithologyGroupSub = await _lithologyGroupSubRepository.GetById(lithologyGroupSubId);
                if (lithologyGroupSub == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<LithologyGroupSubDto>(lithologyGroupSub);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}