using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class ProjectStatusService: IProjectStatusService
    {
        private readonly IProjectStatusRepository _projectStatusRepository;

        private readonly IMapper _mapper;
        
        public ProjectStatusService(IProjectStatusRepository projectStatusRepository,
                           IMapper mapper)
        {
            _projectStatusRepository = projectStatusRepository;
            _mapper = mapper;
        }

        public async Task<ProjectStatusDto> Add(ProjectStatusDto projectStatusDto) 
        {
            try
            {
                //Map Dto > Class
                var addProjectStatus = _mapper.Map<ProjectStatus>(projectStatusDto); 
                //Add ProjectStatus
                var resultCode = await _projectStatusRepository.Add(addProjectStatus); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New ProjectStatus
                var result = await _projectStatusRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectStatusDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<ProjectStatusDto> Update(ProjectStatusDto projectStatusDto) 
        {
            try
            {
                //Check if exist ProjectStatus
                var existProjectStatus = await _projectStatusRepository.GetById(projectStatusDto.Id);
                if (existProjectStatus == null) return null;
                //Map Dto > Class
                var updateProjectStatus = _mapper.Map<ProjectStatus>(projectStatusDto);
                //Update ProjectStatus
                var resultCode = await _projectStatusRepository.Update(updateProjectStatus); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated ProjectStatus
                var result = await _projectStatusRepository.GetById(updateProjectStatus.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectStatusDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int projectStatusId) 
        {
            try
            {
                return await _projectStatusRepository.Delete(projectStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<ProjectStatusDto>> Get(PageParams pageParams) 
        {
            try
            {
                var projectStatuss = await _projectStatusRepository.Get(pageParams);
                if (projectStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectStatusDto>>(projectStatuss);
                result.TotalCount  = projectStatuss.TotalCount;
                result.CurrentPage = projectStatuss.CurrentPage;
                result.PageSize    = projectStatuss.PageSize;
                result.TotalPages  = projectStatuss.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<ProjectStatusDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var projectStatuss = await _projectStatusRepository.GetByAccount(accountId, pageParams);
                if (projectStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectStatusDto>>(projectStatuss);
                result.TotalCount  = projectStatuss.TotalCount;
                result.CurrentPage = projectStatuss.CurrentPage;
                result.PageSize    = projectStatuss.PageSize;
                result.TotalPages  = projectStatuss.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<ProjectStatusDto> GetById(int projectStatusId) 
        {
            try
            {
                var projectStatus = await _projectStatusRepository.GetById(projectStatusId);
                if (projectStatus == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<ProjectStatusDto>(projectStatus);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}