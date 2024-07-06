using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxService: IDrillBoxService
    {
        private readonly IDrillBoxRepository _drillBoxRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxService(IDrillBoxRepository drillBoxRepository,
                           IMapper mapper)
        {
            _drillBoxRepository = drillBoxRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxDto> Add(DrillBoxDto drillBoxDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBox = _mapper.Map<DrillBox>(drillBoxDto); 
                //Add DrillBox
                var resultCode = await _drillBoxRepository.Add(addDrillBox); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBox
                var result = await _drillBoxRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxDto> Update(DrillBoxDto drillBoxDto) 
        {
            try
            {
                //Check if exist DrillBox
                var existDrillBox = await _drillBoxRepository.GetById(drillBoxDto.Id);
                if (existDrillBox == null) return null;
                //Map Dto > Class
                var updateDrillBox = _mapper.Map<DrillBox>(drillBoxDto);
                //Update DrillBox
                var resultCode = await _drillBoxRepository.Update(updateDrillBox); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBox
                var result = await _drillBoxRepository.GetById(updateDrillBox.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxId) 
        {
            try
            {
                return await _drillBoxRepository.Delete(drillBoxId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.Get(pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByAccount(accountId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByRegion(int regionId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByRegion(regionId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByDeposit(int depositId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByDeposit(depositId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByMine(int mineId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByMine(mineId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByMineArea(int mineAreaId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByMineArea(mineAreaId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxDto>> GetByDrillHole(int drillHoleId, PageParams pageParams) 
        {
            try
            {
                var drillBoxes = await _drillBoxRepository.GetByDrillHole(drillHoleId, pageParams);
                if (drillBoxes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxDto>>(drillBoxes);
                result.TotalCount  = drillBoxes.TotalCount;
                result.CurrentPage = drillBoxes.CurrentPage;
                result.PageSize    = drillBoxes.PageSize;
                result.TotalPages  = drillBoxes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxDto> GetById(int drillBoxId) 
        {
            try
            {
                var drillBox = await _drillBoxRepository.GetById(drillBoxId);
                if (drillBox == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxDto>(drillBox);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}