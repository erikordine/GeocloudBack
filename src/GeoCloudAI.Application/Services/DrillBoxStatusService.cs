using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxStatusService: IDrillBoxStatusService
    {
        private readonly IDrillBoxStatusRepository _drillBoxStatusRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxStatusService(IDrillBoxStatusRepository drillBoxStatusRepository,
                           IMapper mapper)
        {
            _drillBoxStatusRepository = drillBoxStatusRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxStatusDto> Add(DrillBoxStatusDto drillBoxStatusDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBoxStatus = _mapper.Map<DrillBoxStatus>(drillBoxStatusDto); 
                //Add DrillBoxStatus
                var resultCode = await _drillBoxStatusRepository.Add(addDrillBoxStatus); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBoxStatus
                var result = await _drillBoxStatusRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxStatusDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxStatusDto> Update(DrillBoxStatusDto drillBoxStatusDto) 
        {
            try
            {
                //Check if exist DrillBoxStatus
                var existDrillBoxStatus = await _drillBoxStatusRepository.GetById(drillBoxStatusDto.Id);
                if (existDrillBoxStatus == null) return null;
                //Map Dto > Class
                var updateDrillBoxStatus = _mapper.Map<DrillBoxStatus>(drillBoxStatusDto);
                //Update DrillBoxStatus
                var resultCode = await _drillBoxStatusRepository.Update(updateDrillBoxStatus); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBoxStatus
                var result = await _drillBoxStatusRepository.GetById(updateDrillBoxStatus.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxStatusDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxStatusId) 
        {
            try
            {
                return await _drillBoxStatusRepository.Delete(drillBoxStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxStatusDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxStatuss = await _drillBoxStatusRepository.Get(pageParams);
                if (drillBoxStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxStatusDto>>(drillBoxStatuss);
                result.TotalCount  = drillBoxStatuss.TotalCount;
                result.CurrentPage = drillBoxStatuss.CurrentPage;
                result.PageSize    = drillBoxStatuss.PageSize;
                result.TotalPages  = drillBoxStatuss.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxStatusDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxStatuss = await _drillBoxStatusRepository.GetByAccount(accountId, pageParams);
                if (drillBoxStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxStatusDto>>(drillBoxStatuss);
                result.TotalCount  = drillBoxStatuss.TotalCount;
                result.CurrentPage = drillBoxStatuss.CurrentPage;
                result.PageSize    = drillBoxStatuss.PageSize;
                result.TotalPages  = drillBoxStatuss.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxStatusDto> GetById(int drillBoxStatusId) 
        {
            try
            {
                var drillBoxStatus = await _drillBoxStatusRepository.GetById(drillBoxStatusId);
                if (drillBoxStatus == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxStatusDto>(drillBoxStatus);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}