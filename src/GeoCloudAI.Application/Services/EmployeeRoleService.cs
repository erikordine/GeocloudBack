using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class EmployeeRoleService: IEmployeeRoleService
    {
        private readonly IEmployeeRoleRepository _employeeRoleRepository;

        private readonly IMapper _mapper;
        
        public EmployeeRoleService(IEmployeeRoleRepository employeeRoleRepository,
                           IMapper mapper)
        {
            _employeeRoleRepository = employeeRoleRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeRoleDto> Add(EmployeeRoleDto employeeRoleDto) 
        {
            try
            {
                //Map Dto > Class
                var addEmployeeRole = _mapper.Map<Domain.Classes.EmployeeRole>(employeeRoleDto); 
                //Add EmployeeRole
                var resultCode = await _employeeRoleRepository.Add(addEmployeeRole); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New EmployeeRole
                var result = await _employeeRoleRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<EmployeeRoleDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<EmployeeRoleDto> Update(EmployeeRoleDto employeeRoleDto) 
        {
            try
            {
                //Check if exist EmployeeRole
                var existEmployeeRole = await _employeeRoleRepository.GetById(employeeRoleDto.Id);
                if (existEmployeeRole == null) return null;
                //Map Dto > Class
                var updateEmployeeRole = _mapper.Map<Domain.Classes.EmployeeRole>(employeeRoleDto);
                //Update EmployeeRole
                var resultCode = await _employeeRoleRepository.Update(updateEmployeeRole); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated EmployeeRole
                var result = await _employeeRoleRepository.GetById(updateEmployeeRole.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<EmployeeRoleDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int employeeRoleId) 
        {
            try
            {
                return await _employeeRoleRepository.Delete(employeeRoleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<EmployeeRoleDto>> Get(PageParams pageParams) 
        {
            try
            {
                var employeeRoles = await _employeeRoleRepository.Get(pageParams);
                if (employeeRoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<EmployeeRoleDto>>(employeeRoles);
                result.TotalCount  = employeeRoles.TotalCount;
                result.CurrentPage = employeeRoles.CurrentPage;
                result.PageSize    = employeeRoles.PageSize;
                result.TotalPages  = employeeRoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<EmployeeRoleDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var employeeRoles = await _employeeRoleRepository.GetByAccount(accountId, pageParams);
                if (employeeRoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<EmployeeRoleDto>>(employeeRoles);
                result.TotalCount  = employeeRoles.TotalCount;
                result.CurrentPage = employeeRoles.CurrentPage;
                result.PageSize    = employeeRoles.PageSize;
                result.TotalPages  = employeeRoles.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<EmployeeRoleDto> GetById(int employeeRoleId) 
        {
            try
            {
                var employeeRole = await _employeeRoleRepository.GetById(employeeRoleId);
                if (employeeRole == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<EmployeeRoleDto>(employeeRole);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}