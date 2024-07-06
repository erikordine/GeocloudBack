using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IMapper _mapper;
        
        public RoleService(IRoleRepository roleRepository,
                           IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> Add(RoleDto roleDto) 
        {
            try
            {
                //Map Dto > Class
                var addRole = _mapper.Map<Domain.Classes.Role>(roleDto); 
                //Add Role
                var resultCode = await _roleRepository.Add(addRole); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Role
                var result = await _roleRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<RoleDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<RoleDto> Update(RoleDto roleDto) 
        {
            try
            {
                //Check if exist Role
                var existRole = await _roleRepository.GetById(roleDto.Id);
                if (existRole == null) return null;
                //Map Dto > Class
                var updateRole = _mapper.Map<Domain.Classes.Role>(roleDto);
                //Update Role
                var resultCode = await _roleRepository.Update(updateRole); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Role
                var result = await _roleRepository.GetById(updateRole.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<RoleDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int roleId) 
        {
            try
            {
                return await _roleRepository.Delete(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<RoleDto>> Get(PageParams pageParams) 
        {
            try
            {
                var roles = await _roleRepository.Get(pageParams);
                if (roles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<RoleDto>>(roles);
                result.TotalCount  = roles.TotalCount;
                result.CurrentPage = roles.CurrentPage;
                result.PageSize    = roles.PageSize;
                result.TotalPages  = roles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<RoleDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var roles = await _roleRepository.GetByAccount(accountId, pageParams);
                if (roles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<RoleDto>>(roles);
                result.TotalCount  = roles.TotalCount;
                result.CurrentPage = roles.CurrentPage;
                result.PageSize    = roles.PageSize;
                result.TotalPages  = roles.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<RoleDto> GetById(int roleId) 
        {
            try
            {
                var role = await _roleRepository.GetById(roleId);
                if (role == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<RoleDto>(role);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}