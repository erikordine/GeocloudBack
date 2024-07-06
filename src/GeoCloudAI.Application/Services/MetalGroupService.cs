using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class MetalGroupService: IMetalGroupService
    {
        private readonly IMetalGroupRepository _metalGroupRepository;

        private readonly IMapper _mapper;
        
        public MetalGroupService(IMetalGroupRepository metalGroupRepository,
                           IMapper mapper)
        {
            _metalGroupRepository = metalGroupRepository;
            _mapper = mapper;
        }

        public async Task<MetalGroupDto> Add(MetalGroupDto metalGroupDto) 
        {
            try
            {
                //Map Dto > Class
                var addMetalGroup = _mapper.Map<Domain.Classes.MetalGroup>(metalGroupDto); 
                //Add MetalGroup
                var resultCode = await _metalGroupRepository.Add(addMetalGroup); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MetalGroup
                var result = await _metalGroupRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MetalGroupDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MetalGroupDto> Update(MetalGroupDto metalGroupDto) 
        {
            try
            {
                //Check if exist MetalGroup
                var existMetalGroup = await _metalGroupRepository.GetById(metalGroupDto.Id);
                if (existMetalGroup == null) return null;
                //Map Dto > Class
                var updateMetalGroup = _mapper.Map<Domain.Classes.MetalGroup>(metalGroupDto);
                //Update MetalGroup
                var resultCode = await _metalGroupRepository.Update(updateMetalGroup); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MetalGroup
                var result = await _metalGroupRepository.GetById(updateMetalGroup.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MetalGroupDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int metalGroupId) 
        {
            try
            {
                return await _metalGroupRepository.Delete(metalGroupId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MetalGroupDto>> Get(PageParams pageParams) 
        {
            try
            {
                var metalGroups = await _metalGroupRepository.Get(pageParams);
                if (metalGroups == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupDto>>(metalGroups);
                result.TotalCount  = metalGroups.TotalCount;
                result.CurrentPage = metalGroups.CurrentPage;
                result.PageSize    = metalGroups.PageSize;
                result.TotalPages  = metalGroups.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MetalGroupDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var metalGroups = await _metalGroupRepository.GetByAccount(accountId, pageParams);
                if (metalGroups == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupDto>>(metalGroups);
                result.TotalCount  = metalGroups.TotalCount;
                result.CurrentPage = metalGroups.CurrentPage;
                result.PageSize    = metalGroups.PageSize;
                result.TotalPages  = metalGroups.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MetalGroupDto>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams) 
        {
            try
            {
                var metalGroups = await _metalGroupRepository.GetByOreGeneticType(oreGeneticTypeId, pageParams);
                if (metalGroups == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MetalGroupDto>>(metalGroups);
                result.TotalCount  = metalGroups.TotalCount;
                result.CurrentPage = metalGroups.CurrentPage;
                result.PageSize    = metalGroups.PageSize;
                result.TotalPages  = metalGroups.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MetalGroupDto> GetById(int metalGroupId) 
        {
            try
            {
                var metalGroup = await _metalGroupRepository.GetById(metalGroupId);
                if (metalGroup == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MetalGroupDto>(metalGroup);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}