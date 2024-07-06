using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class MetalGroupSubService: IMetalGroupSubService
    {
        private readonly IMetalGroupSubRepository _metalGroupSubRepository;

        private readonly IMapper _mapper;
        
        public MetalGroupSubService(IMetalGroupSubRepository metalGroupSubRepository,
                           IMapper mapper)
        {
            _metalGroupSubRepository = metalGroupSubRepository;
            _mapper = mapper;
        }

        public async Task<MetalGroupSubDto> Add(MetalGroupSubDto metalGroupSubDto) 
        {
            try
            {
                //Map Dto > Class
                var addMetalGroupSub = _mapper.Map<Domain.Classes.MetalGroupSub>(metalGroupSubDto); 
                //Add MetalGroupSub
                var resultCode = await _metalGroupSubRepository.Add(addMetalGroupSub); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MetalGroupSub
                var result = await _metalGroupSubRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MetalGroupSubDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MetalGroupSubDto> Update(MetalGroupSubDto metalGroupSubDto) 
        {
            try
            {
                //Check if exist MetalGroupSub
                var existMetalGroupSub = await _metalGroupSubRepository.GetById(metalGroupSubDto.Id);
                if (existMetalGroupSub == null) return null;
                //Map Dto > Class
                var updateMetalGroupSub = _mapper.Map<Domain.Classes.MetalGroupSub>(metalGroupSubDto);
                //Update MetalGroupSub
                var resultCode = await _metalGroupSubRepository.Update(updateMetalGroupSub); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MetalGroupSub
                var result = await _metalGroupSubRepository.GetById(updateMetalGroupSub.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MetalGroupSubDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int metalGroupSubId) 
        {
            try
            {
                return await _metalGroupSubRepository.Delete(metalGroupSubId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MetalGroupSubDto>> Get(PageParams pageParams) 
        {
            try
            {
                var metalGroupSubs = await _metalGroupSubRepository.Get(pageParams);
                if (metalGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupSubDto>>(metalGroupSubs);
                result.TotalCount  = metalGroupSubs.TotalCount;
                result.CurrentPage = metalGroupSubs.CurrentPage;
                result.PageSize    = metalGroupSubs.PageSize;
                result.TotalPages  = metalGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MetalGroupSubDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var metalGroupSubs = await _metalGroupSubRepository.GetByAccount(accountId, pageParams);
                if (metalGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupSubDto>>(metalGroupSubs);
                result.TotalCount  = metalGroupSubs.TotalCount;
                result.CurrentPage = metalGroupSubs.CurrentPage;
                result.PageSize    = metalGroupSubs.PageSize;
                result.TotalPages  = metalGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MetalGroupSubDto>> GetByMetalGroup(int metalGroupId, PageParams pageParams) 
        {
            try
            {
                var metalGroupSubs = await _metalGroupSubRepository.GetByMetalGroup(metalGroupId, pageParams);
                if (metalGroupSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupSubDto>>(metalGroupSubs);
                result.TotalCount  = metalGroupSubs.TotalCount;
                result.CurrentPage = metalGroupSubs.CurrentPage;
                result.PageSize    = metalGroupSubs.PageSize;
                result.TotalPages  = metalGroupSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MetalGroupSubDto> GetById(int metalGroupSubId) 
        {
            try
            {
                var metalGroupSub = await _metalGroupSubRepository.GetById(metalGroupSubId);
                if (metalGroupSub == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MetalGroupSubDto>(metalGroupSub);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}