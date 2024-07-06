using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class MineAreaService: IMineAreaService
    {
        private readonly IMineAreaRepository _mineAreaRepository;

        private readonly IMapper _mapper;
        
        public MineAreaService(IMineAreaRepository mineAreaRepository,
                               IMapper mapper)
        {
            _mineAreaRepository = mineAreaRepository;
            _mapper = mapper;
        }

        public async Task<MineAreaDto> Add(MineAreaDto mineAreaDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineArea = _mapper.Map<MineArea>(mineAreaDto); 
                //Add MineArea
                var resultCode = await _mineAreaRepository.Add(addMineArea); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineArea
                var result = await _mineAreaRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineAreaDto> Update(MineAreaDto mineAreaDto) 
        {
            try
            {
                //Check if exist MineArea
                var existMineArea = await _mineAreaRepository.GetById(mineAreaDto.Id);
                if (existMineArea == null) return null;
                //Map Dto > Class
                var updateMineArea = _mapper.Map<MineArea>(mineAreaDto);
                //Update MineArea
                var resultCode = await _mineAreaRepository.Update(updateMineArea); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineArea
                var result = await _mineAreaRepository.GetById(updateMineArea.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineAreaId) 
        {
            try
            {
                return await _mineAreaRepository.Delete(mineAreaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineAreaDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineAreas = await _mineAreaRepository.Get(pageParams);
                if (mineAreas == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaDto>>(mineAreas);
                result.TotalCount  = mineAreas.TotalCount;
                result.CurrentPage = mineAreas.CurrentPage;
                result.PageSize    = mineAreas.PageSize;
                result.TotalPages  = mineAreas.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineAreas = await _mineAreaRepository.GetByAccount(accountId, pageParams);
                if (mineAreas == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaDto>>(mineAreas);
                result.TotalCount  = mineAreas.TotalCount;
                result.CurrentPage = mineAreas.CurrentPage;
                result.PageSize    = mineAreas.PageSize;
                result.TotalPages  = mineAreas.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaDto>> GetByRegion(int regionId, PageParams pageParams) 
        {
            try
            {
                var mineAreas = await _mineAreaRepository.GetByRegion(regionId, pageParams);
                if (mineAreas == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaDto>>(mineAreas);
                result.TotalCount  = mineAreas.TotalCount;
                result.CurrentPage = mineAreas.CurrentPage;
                result.PageSize    = mineAreas.PageSize;
                result.TotalPages  = mineAreas.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaDto>> GetByDeposit(int depositId, PageParams pageParams) 
        {
            try
            {
                var mineAreas = await _mineAreaRepository.GetByDeposit(depositId, pageParams);
                if (mineAreas == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaDto>>(mineAreas);
                result.TotalCount  = mineAreas.TotalCount;
                result.CurrentPage = mineAreas.CurrentPage;
                result.PageSize    = mineAreas.PageSize;
                result.TotalPages  = mineAreas.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaDto>> GetByMine(int mineId, PageParams pageParams) 
        {
            try
            {
                var mineAreas = await _mineAreaRepository.GetByMine(mineId, pageParams);
                if (mineAreas == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaDto>>(mineAreas);
                result.TotalCount  = mineAreas.TotalCount;
                result.CurrentPage = mineAreas.CurrentPage;
                result.PageSize    = mineAreas.PageSize;
                result.TotalPages  = mineAreas.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineAreaDto> GetById(int mineAreaId) 
        {
            try
            {
                var mineArea = await _mineAreaRepository.GetById(mineAreaId);
                if (mineArea == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineAreaDto>(mineArea);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}