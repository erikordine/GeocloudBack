using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class MineService: IMineService
    {
        private readonly IMineRepository _mineRepository;

        private readonly IMapper _mapper;
        
        public MineService(IMineRepository mineRepository,
                           IMapper mapper)
        {
            _mineRepository = mineRepository;
            _mapper = mapper;
        }

        public async Task<MineDto> Add(MineDto mineDto) 
        {
            try
            {
                //Map Dto > Class
                var addMine = _mapper.Map<Mine>(mineDto); 
                //Add Mine
                var resultCode = await _mineRepository.Add(addMine); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Mine
                var result = await _mineRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineDto> Update(MineDto mineDto) 
        {
            try
            {
                //Check if exist Mine
                var existMine = await _mineRepository.GetById(mineDto.Id);
                if (existMine == null) return null;
                //Map Dto > Class
                var updateMine = _mapper.Map<Mine>(mineDto);
                //Update Mine
                var resultCode = await _mineRepository.Update(updateMine); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Mine
                var result = await _mineRepository.GetById(updateMine.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineId) 
        {
            try
            {
                return await _mineRepository.Delete(mineId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mines = await _mineRepository.Get(pageParams);
                if (mines == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineDto>>(mines);
                result.TotalCount  = mines.TotalCount;
                result.CurrentPage = mines.CurrentPage;
                result.PageSize    = mines.PageSize;
                result.TotalPages  = mines.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mines = await _mineRepository.GetByAccount(accountId, pageParams);
                if (mines == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineDto>>(mines);
                result.TotalCount  = mines.TotalCount;
                result.CurrentPage = mines.CurrentPage;
                result.PageSize    = mines.PageSize;
                result.TotalPages  = mines.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineDto>> GetByRegion(int regionId, PageParams pageParams) 
        {
            try
            {
                var mines = await _mineRepository.GetByRegion(regionId, pageParams);
                if (mines == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineDto>>(mines);
                result.TotalCount  = mines.TotalCount;
                result.CurrentPage = mines.CurrentPage;
                result.PageSize    = mines.PageSize;
                result.TotalPages  = mines.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineDto>> GetByDeposit(int depositId, PageParams pageParams) 
        {
            try
            {
                var mines = await _mineRepository.GetByDeposit(depositId, pageParams);
                if (mines == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineDto>>(mines);
                result.TotalCount  = mines.TotalCount;
                result.CurrentPage = mines.CurrentPage;
                result.PageSize    = mines.PageSize;
                result.TotalPages  = mines.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineDto> GetById(int mineId) 
        {
            try
            {
                var mine = await _mineRepository.GetById(mineId);
                if (mine == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineDto>(mine);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}