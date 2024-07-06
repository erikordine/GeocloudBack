using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class ProjectTypeService: IProjectTypeService
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        private readonly IMapper _mapper;
        
        public ProjectTypeService(IProjectTypeRepository projectTypeRepository,
                           IMapper mapper)
        {
            _projectTypeRepository = projectTypeRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTypeDto> Add(ProjectTypeDto projectTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addProjectType = _mapper.Map<Domain.Classes.ProjectType>(projectTypeDto); 
                //Add ProjectType
                var resultCode = await _projectTypeRepository.Add(addProjectType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New ProjectType
                var result = await _projectTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<ProjectTypeDto> Update(ProjectTypeDto projectTypeDto) 
        {
            try
            {
                //Check if exist ProjectType
                var existProjectType = await _projectTypeRepository.GetById(projectTypeDto.Id);
                if (existProjectType == null) return null;
                //Map Dto > Class
                var updateProjectType = _mapper.Map<Domain.Classes.ProjectType>(projectTypeDto);
                //Update ProjectType
                var resultCode = await _projectTypeRepository.Update(updateProjectType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated ProjectType
                var result = await _projectTypeRepository.GetById(updateProjectType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<ProjectTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int projectTypeId) 
        {
            try
            {
                return await _projectTypeRepository.Delete(projectTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<ProjectTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var projectTypes = await _projectTypeRepository.Get(pageParams);
                if (projectTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectTypeDto>>(projectTypes);
                result.TotalCount  = projectTypes.TotalCount;
                result.CurrentPage = projectTypes.CurrentPage;
                result.PageSize    = projectTypes.PageSize;
                result.TotalPages  = projectTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<ProjectTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var projectTypes = await _projectTypeRepository.GetByAccount(accountId, pageParams);
                if (projectTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<ProjectTypeDto>>(projectTypes);
                result.TotalCount  = projectTypes.TotalCount;
                result.CurrentPage = projectTypes.CurrentPage;
                result.PageSize    = projectTypes.PageSize;
                result.TotalPages  = projectTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<ProjectTypeDto> GetById(int projectTypeId) 
        {
            try
            {
                var projectType = await _projectTypeRepository.GetById(projectTypeId);
                if (projectType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<ProjectTypeDto>(projectType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}