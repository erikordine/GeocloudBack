using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class LithologyGroupService: ILithologyGroupService
    {
        private readonly ILithologyGroupRepository _lithologyGroupRepository;

        private readonly IMapper _mapper;
        
        public LithologyGroupService(ILithologyGroupRepository lithologyGroupRepository,
                           IMapper mapper)
        {
            _lithologyGroupRepository = lithologyGroupRepository;
            _mapper = mapper;
        }

        public async Task<LithologyGroupDto> Add(LithologyGroupDto lithologyGroupDto) 
        {
            try
            {
                //Map Dto > Class
                var addLithologyGroup = _mapper.Map<Domain.Classes.LithologyGroup>(lithologyGroupDto); 
                //Add LithologyGroup
                var resultCode = await _lithologyGroupRepository.Add(addLithologyGroup); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New LithologyGroup
                var result = await _lithologyGroupRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyGroupDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<LithologyGroupDto> Update(LithologyGroupDto lithologyGroupDto) 
        {
            try
            {
                //Check if exist LithologyGroup
                var existLithologyGroup = await _lithologyGroupRepository.GetById(lithologyGroupDto.Id);
                if (existLithologyGroup == null) return null;
                //Map Dto > Class
                var updateLithologyGroup = _mapper.Map<Domain.Classes.LithologyGroup>(lithologyGroupDto);
                //Update LithologyGroup
                var resultCode = await _lithologyGroupRepository.Update(updateLithologyGroup); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated LithologyGroup
                var result = await _lithologyGroupRepository.GetById(updateLithologyGroup.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<LithologyGroupDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int lithologyGroupId) 
        {
            try
            {
                return await _lithologyGroupRepository.Delete(lithologyGroupId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<LithologyGroupDto>> Get(PageParams pageParams) 
        {
            try
            {
                var lithologyGroups = await _lithologyGroupRepository.Get(pageParams);
                if (lithologyGroups == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyGroupDto>>(lithologyGroups);
                result.TotalCount  = lithologyGroups.TotalCount;
                result.CurrentPage = lithologyGroups.CurrentPage;
                result.PageSize    = lithologyGroups.PageSize;
                result.TotalPages  = lithologyGroups.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<LithologyGroupDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var lithologyGroups = await _lithologyGroupRepository.GetByAccount(accountId, pageParams);
                if (lithologyGroups == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<LithologyGroupDto>>(lithologyGroups);
                result.TotalCount  = lithologyGroups.TotalCount;
                result.CurrentPage = lithologyGroups.CurrentPage;
                result.PageSize    = lithologyGroups.PageSize;
                result.TotalPages  = lithologyGroups.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<LithologyGroupDto> GetById(int lithologyGroupId) 
        {
            try
            {
                var lithologyGroup = await _lithologyGroupRepository.GetById(lithologyGroupId);
                if (lithologyGroup == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<LithologyGroupDto>(lithologyGroup);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}