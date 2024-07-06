using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IMapper _mapper;
        
        public ProjectService(IProjectRepository projectRepository,
                           IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Add(ProjectDto projectDto) 
        {
            try
            {
                //Map Dto > Class
                var addProject = _mapper.Map<Domain.Classes.Project>(projectDto); 
                //Add Project
                var resultCode = await _projectRepository.Add(addProject); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Project
                var result = await _projectRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<ProjectDto> Update(ProjectDto projectDto) 
        {
            try
            {
                //Check if exist Project
                var existProject = await _projectRepository.GetById(projectDto.Id);
                if (existProject == null) return null;
                //Map Dto > Class
                var updateProject = _mapper.Map<Domain.Classes.Project>(projectDto);
                //Update Project
                var resultCode = await _projectRepository.Update(updateProject); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Project
                var result = await _projectRepository.GetById(updateProject.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int projectId) 
        {
            try
            {
                return await _projectRepository.Delete(projectId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<ProjectDto>> Get(PageParams pageParams) 
        {
            try
            {
                var projects = await _projectRepository.Get(pageParams);
                if (projects == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectDto>>(projects);
                result.TotalCount  = projects.TotalCount;
                result.CurrentPage = projects.CurrentPage;
                result.PageSize    = projects.PageSize;
                result.TotalPages  = projects.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        
        public async Task<PageList<ProjectDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var projects = await _projectRepository.GetByAccount(accountId, pageParams);
                if (projects == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectDto>>(projects);
                result.TotalCount  = projects.TotalCount;
                result.CurrentPage = projects.CurrentPage;
                result.PageSize    = projects.PageSize;
                result.TotalPages  = projects.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<ProjectDto> GetById(int projectId) 
        {
            try
            {
                var project = await _projectRepository.GetById(projectId);
                if (project == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<ProjectDto>(project);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}