using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DrillHoleService: IDrillHoleService
    {
        private readonly IDrillHoleRepository _drillHoleRepository;

        private readonly IMapper _mapper;
        
        public DrillHoleService(IDrillHoleRepository drillHoleRepository,
                           IMapper mapper)
        {
            _drillHoleRepository = drillHoleRepository;
            _mapper = mapper;
        }

        public async Task<DrillHoleDto> Add(DrillHoleDto drillHoleDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillHole = _mapper.Map<DrillHole>(drillHoleDto); 
                //Add DrillHole
                var resultCode = await _drillHoleRepository.Add(addDrillHole); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillHole
                var result = await _drillHoleRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillHoleDto> Update(DrillHoleDto drillHoleDto) 
        {
            try
            {
                //Check if exist DrillHole
                var existDrillHole = await _drillHoleRepository.GetById(drillHoleDto.Id);
                if (existDrillHole == null) return null;
                //Map Dto > Class
                var updateDrillHole = _mapper.Map<DrillHole>(drillHoleDto);
                //Update DrillHole
                var resultCode = await _drillHoleRepository.Update(updateDrillHole); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillHole
                var result = await _drillHoleRepository.GetById(updateDrillHole.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillHoleDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillHoleId) 
        {
            try
            {
                return await _drillHoleRepository.Delete(drillHoleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillHoleDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.Get(pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.GetByAccount(accountId, pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleDto>> GetByRegion(int regionId, bool direct, PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.GetByRegion(regionId, direct, pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleDto>> GetByDeposit(int depositId, bool direct, PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.GetByDeposit(depositId, direct, pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleDto>> GetByMine(int mineId, bool direct, PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.GetByMine(mineId, direct, pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillHoleDto>> GetByMineArea(int mineAreaId, PageParams pageParams) 
        {
            try
            {
                var drillHoles = await _drillHoleRepository.GetByMineArea(mineAreaId, pageParams);
                if (drillHoles == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillHoleDto>>(drillHoles);
                result.TotalCount  = drillHoles.TotalCount;
                result.CurrentPage = drillHoles.CurrentPage;
                result.PageSize    = drillHoles.PageSize;
                result.TotalPages  = drillHoles.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillHoleDto> GetById(int drillHoleId) 
        {
            try
            {
                var drillHole = await _drillHoleRepository.GetById(drillHoleId);
                if (drillHole == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillHoleDto>(drillHole);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}