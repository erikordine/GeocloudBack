using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;
        
        public EmployeeService(IEmployeeRepository employeeRepository,
                           IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Add(EmployeeDto employeeDto) 
        {
            try
            {
                //Map Dto > Class
                var addEmployee = _mapper.Map<Employee>(employeeDto); 
                //Add Employee
                var resultCode = await _employeeRepository.Add(addEmployee); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Employee
                var result = await _employeeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<EmployeeDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<EmployeeDto> Update(EmployeeDto employeeDto) 
        {
            try
            {
                //Check if exist Employee
                var existEmployee = await _employeeRepository.GetById(employeeDto.Id);
                if (existEmployee == null) return null;
                //Map Dto > Class
                var updateEmployee = _mapper.Map<Employee>(employeeDto);
                //Update Employee
                var resultCode = await _employeeRepository.Update(updateEmployee); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Employee
                var result = await _employeeRepository.GetById(updateEmployee.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<EmployeeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int employeeId) 
        {
            try
            {
                return await _employeeRepository.Delete(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<EmployeeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var employees = await _employeeRepository.Get(pageParams);
                if (employees == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<EmployeeDto>>(employees);
                result.TotalCount  = employees.TotalCount;
                result.CurrentPage = employees.CurrentPage;
                result.PageSize    = employees.PageSize;
                result.TotalPages  = employees.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<EmployeeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var employees = await _employeeRepository.GetByAccount(accountId, pageParams);
                if (employees == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<EmployeeDto>>(employees);
                result.TotalCount  = employees.TotalCount;
                result.CurrentPage = employees.CurrentPage;
                result.PageSize    = employees.PageSize;
                result.TotalPages  = employees.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<EmployeeDto>> GetByCompany(int companytId, PageParams pageParams) 
        {
            try
            {
                var employees = await _employeeRepository.GetByCompany(companytId, pageParams);
                if (employees == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<EmployeeDto>>(employees);
                result.TotalCount  = employees.TotalCount;
                result.CurrentPage = employees.CurrentPage;
                result.PageSize    = employees.PageSize;
                result.TotalPages  = employees.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<EmployeeDto> GetById(int employeeId) 
        {
            try
            {
                var employee = await _employeeRepository.GetById(employeeId);
                if (employee == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<EmployeeDto>(employee);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}