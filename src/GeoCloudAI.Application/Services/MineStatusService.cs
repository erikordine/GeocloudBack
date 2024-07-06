using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class MineStatusService: IMineStatusService
    {
        private readonly IMineStatusRepository _mineStatusRepository;

        private readonly IMapper _mapper;
        
        public MineStatusService(IMineStatusRepository mineStatusRepository,
                           IMapper mapper)
        {
            _mineStatusRepository = mineStatusRepository;
            _mapper = mapper;
        }

        public async Task<MineStatusDto> Add(MineStatusDto mineStatusDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineStatus = _mapper.Map<MineStatus>(mineStatusDto); 
                //Add MineStatus
                var resultCode = await _mineStatusRepository.Add(addMineStatus); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineStatus
                var result = await _mineStatusRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineStatusDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineStatusDto> Update(MineStatusDto mineStatusDto) 
        {
            try
            {
                //Check if exist MineStatus
                var existMineStatus = await _mineStatusRepository.GetById(mineStatusDto.Id);
                if (existMineStatus == null) return null;
                //Map Dto > Class
                var updateMineStatus = _mapper.Map<MineStatus>(mineStatusDto);
                //Update MineStatus
                var resultCode = await _mineStatusRepository.Update(updateMineStatus); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineStatus
                var result = await _mineStatusRepository.GetById(updateMineStatus.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineStatusDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineStatusId) 
        {
            try
            {
                return await _mineStatusRepository.Delete(mineStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineStatusDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineStatuss = await _mineStatusRepository.Get(pageParams);
                if (mineStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineStatusDto>>(mineStatuss);
                result.TotalCount  = mineStatuss.TotalCount;
                result.CurrentPage = mineStatuss.CurrentPage;
                result.PageSize    = mineStatuss.PageSize;
                result.TotalPages  = mineStatuss.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineStatusDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineStatuss = await _mineStatusRepository.GetByAccount(accountId, pageParams);
                if (mineStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineStatusDto>>(mineStatuss);
                result.TotalCount  = mineStatuss.TotalCount;
                result.CurrentPage = mineStatuss.CurrentPage;
                result.PageSize    = mineStatuss.PageSize;
                result.TotalPages  = mineStatuss.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineStatusDto> GetById(int mineStatusId) 
        {
            try
            {
                var mineStatus = await _mineStatusRepository.GetById(mineStatusId);
                if (mineStatus == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineStatusDto>(mineStatus);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}