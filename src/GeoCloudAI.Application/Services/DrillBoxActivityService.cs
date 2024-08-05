using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxActivityService: IDrillBoxActivityService
    {
        private readonly IDrillBoxActivityRepository _drillBoxActivityRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxActivityService(IDrillBoxActivityRepository drillBoxActivityRepository,
                           IMapper mapper)
        {
            _drillBoxActivityRepository = drillBoxActivityRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxActivityDto> Add(DrillBoxActivityDto drillBoxActivityDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBoxActivity = _mapper.Map<DrillBoxActivity>(drillBoxActivityDto); 
                //Add DrillBoxActivity
                var resultCode = await _drillBoxActivityRepository.Add(addDrillBoxActivity); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBoxActivity
                var result = await _drillBoxActivityRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxActivityDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxActivityDto> Update(DrillBoxActivityDto drillBoxActivityDto) 
        {
            try
            {
                //Check if exist DrillBoxActivity
                var existDrillBoxActivity = await _drillBoxActivityRepository.GetById(drillBoxActivityDto.Id);
                if (existDrillBoxActivity == null) return null;
                //Map Dto > Class
                var updateDrillBoxActivity = _mapper.Map<DrillBoxActivity>(drillBoxActivityDto);
                //Update DrillBoxActivity
                var resultCode = await _drillBoxActivityRepository.Update(updateDrillBoxActivity); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBoxActivity
                var result = await _drillBoxActivityRepository.GetById(updateDrillBoxActivity.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxActivityDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxActivityId) 
        {
            try
            {
                return await _drillBoxActivityRepository.Delete(drillBoxActivityId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxActivityDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxActivities = await _drillBoxActivityRepository.Get(pageParams);
                if (drillBoxActivities == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxActivityDto>>(drillBoxActivities);
                result.TotalCount  = drillBoxActivities.TotalCount;
                result.CurrentPage = drillBoxActivities.CurrentPage;
                result.PageSize    = drillBoxActivities.PageSize;
                result.TotalPages  = drillBoxActivities.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxActivityDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxActivities = await _drillBoxActivityRepository.GetByAccount(accountId, pageParams);
                if (drillBoxActivities == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxActivityDto>>(drillBoxActivities);
                result.TotalCount  = drillBoxActivities.TotalCount;
                result.CurrentPage = drillBoxActivities.CurrentPage;
                result.PageSize    = drillBoxActivities.PageSize;
                result.TotalPages  = drillBoxActivities.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxActivityDto>> GetByDrillBox(int drillBoxId, PageParams pageParams) 
        {
            try
            {
                var drillBoxActivities = await _drillBoxActivityRepository.GetByDrillBox(drillBoxId, pageParams);
                if (drillBoxActivities == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxActivityDto>>(drillBoxActivities);
                result.TotalCount  = drillBoxActivities.TotalCount;
                result.CurrentPage = drillBoxActivities.CurrentPage;
                result.PageSize    = drillBoxActivities.PageSize;
                result.TotalPages  = drillBoxActivities.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxActivityDto> GetById(int drillBoxActivityId) 
        {
            try
            {
                var drillBoxActivity = await _drillBoxActivityRepository.GetById(drillBoxActivityId);
                if (drillBoxActivity == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxActivityDto>(drillBoxActivity);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}