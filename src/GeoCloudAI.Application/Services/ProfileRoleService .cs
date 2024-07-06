using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class ProfileRoleService: IProfileRoleService
    {
        private readonly IProfileRoleRepository _profileRoleRepository;

        private readonly IMapper _mapper;
        
        public ProfileRoleService(IProfileRoleRepository profileRoleRepository,
                           IMapper mapper)
        {
            _profileRoleRepository = profileRoleRepository;
            _mapper = mapper;
        }

        public async Task<ProfileRoleDto> Add(ProfileRoleDto profileRoleDto) 
        {
            try
            {
                //Map Dto > Class
                var addProfileRole = _mapper.Map<Domain.Classes.ProfileRole>(profileRoleDto); 
                //Add ProfileRole
                var resultCode = await _profileRoleRepository.Add(addProfileRole); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New ProfileRole
                var result = await _profileRoleRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProfileRoleDto>(result);
                return resultDto;  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int profileRoleId) 
        {
            try
            {
                return await _profileRoleRepository.Delete(profileRoleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<ProfileRoleDto>> Get(PageParams pageParams) 
        {
            try
            {
                var profileRoles = await _profileRoleRepository.Get(pageParams);
                if (profileRoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProfileRoleDto>>(profileRoles);
                result.TotalCount  = profileRoles.TotalCount;
                result.CurrentPage = profileRoles.CurrentPage;
                result.PageSize    = profileRoles.PageSize;
                result.TotalPages  = profileRoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<ProfileRoleDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var profileRoles = await _profileRoleRepository.GetByAccount(accountId, pageParams);
                if (profileRoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProfileRoleDto>>(profileRoles);
                result.TotalCount  = profileRoles.TotalCount;
                result.CurrentPage = profileRoles.CurrentPage;
                result.PageSize    = profileRoles.PageSize;
                result.TotalPages  = profileRoles.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<ProfileRoleDto>> GetByProfile(int profileId, PageParams pageParams) 
        {
            try
            {
                var profileRoles = await _profileRoleRepository.GetByProfile(profileId, pageParams);
                if (profileRoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProfileRoleDto>>(profileRoles);
                result.TotalCount  = profileRoles.TotalCount;
                result.CurrentPage = profileRoles.CurrentPage;
                result.PageSize    = profileRoles.PageSize;
                result.TotalPages  = profileRoles.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<ProfileRoleDto> GetById(int profileRoleId) 
        {
            try
            {
                var profileRole = await _profileRoleRepository.GetById(profileRoleId);
                if (profileRole == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<ProfileRoleDto>(profileRole);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

    }
}