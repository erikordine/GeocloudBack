using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using AutoMapper;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillHoleRunService: IDrillHoleRunService
    {
        private readonly IDrillHoleRunRepository _drillHoleRunRepository;

        private readonly IMapper _mapper;
        
        public DrillHoleRunService(IDrillHoleRunRepository drillHoleRunRepository,
                           IMapper mapper)
        {
            _drillHoleRunRepository = drillHoleRunRepository;
            _mapper = mapper;
        }
        
        public async Task<DrillHoleRunDto> Add(DrillHoleRunDto drillHoleRunDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillHoleRun = _mapper.Map<DrillHoleRun>(drillHoleRunDto); 
                //Add DrillHoleRun
                var resultCode = await _drillHoleRunRepository.Add(addDrillHoleRun); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillHoleRun
                var result = await _drillHoleRunRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleRunDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillHoleRunDto> Update(DrillHoleRunDto drillHoleRunDto) 
        {
            try
            {
                //Check if exist DrillHoleRun
                var existDrillHoleRun = await _drillHoleRunRepository.GetById(drillHoleRunDto.Id);
                if (existDrillHoleRun == null) return null;
                //Map Dto > Class
                var updateDrillHoleRun = _mapper.Map<DrillHoleRun>(drillHoleRunDto);
                //Update DrillHoleRun
                var resultCode = await _drillHoleRunRepository.Update(updateDrillHoleRun); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillHoleRun
                var result = await _drillHoleRunRepository.GetById(updateDrillHoleRun.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleRunDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillHoleRunId) 
        {
            try
            {
                return await _drillHoleRunRepository.Delete(drillHoleRunId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        public async Task<PageList<DrillHoleRunDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillHoleRuns = await _drillHoleRunRepository.Get(pageParams);
                if (drillHoleRuns == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleRunDto>>(drillHoleRuns);
                result.TotalCount  = drillHoleRuns.TotalCount;
                result.CurrentPage = drillHoleRuns.CurrentPage;
                result.PageSize    = drillHoleRuns.PageSize;
                result.TotalPages  = drillHoleRuns.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleRunDto>> GetByDrillHole(int drillHoleId, PageParams pageParams) 
        {
            try
            {
                var drillHoleRuns = await _drillHoleRunRepository.GetByDrillHole(drillHoleId, pageParams);
                if (drillHoleRuns == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleRunDto>>(drillHoleRuns);
                result.TotalCount  = drillHoleRuns.TotalCount;
                result.CurrentPage = drillHoleRuns.CurrentPage;
                result.PageSize    = drillHoleRuns.PageSize;
                result.TotalPages  = drillHoleRuns.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillHoleRunDto> GetById(int drillHoleRunId) 
        {
            try
            {
                var drillHoleRun = await _drillHoleRunRepository.GetById(drillHoleRunId);
                if (drillHoleRun == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillHoleRunDto>(drillHoleRun);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}