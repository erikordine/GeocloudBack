using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class MineAreaStatusService: IMineAreaStatusService
    {
        private readonly IMineAreaStatusRepository _mineAreaStatusRepository;

        private readonly IMapper _mapper;
        
        public MineAreaStatusService(IMineAreaStatusRepository mineAreaStatusRepository,
                           IMapper mapper)
        {
            _mineAreaStatusRepository = mineAreaStatusRepository;
            _mapper = mapper;
        }

        public async Task<MineAreaStatusDto> Add(MineAreaStatusDto mineAreaStatusDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineAreaStatus = _mapper.Map<MineAreaStatus>(mineAreaStatusDto); 
                //Add MineAreaStatus
                var resultCode = await _mineAreaStatusRepository.Add(addMineAreaStatus); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineAreaStatus
                var result = await _mineAreaStatusRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaStatusDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineAreaStatusDto> Update(MineAreaStatusDto mineAreaStatusDto) 
        {
            try
            {
                //Check if exist MineAreaStatus
                var existMineAreaStatus = await _mineAreaStatusRepository.GetById(mineAreaStatusDto.Id);
                if (existMineAreaStatus == null) return null;
                //Map Dto > Class
                var updateMineAreaStatus = _mapper.Map<MineAreaStatus>(mineAreaStatusDto);
                //Update MineAreaStatus
                var resultCode = await _mineAreaStatusRepository.Update(updateMineAreaStatus); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineAreaStatus
                var result = await _mineAreaStatusRepository.GetById(updateMineAreaStatus.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaStatusDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineAreaStatusId) 
        {
            try
            {
                return await _mineAreaStatusRepository.Delete(mineAreaStatusId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineAreaStatusDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineAreaStatuss = await _mineAreaStatusRepository.Get(pageParams);
                if (mineAreaStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaStatusDto>>(mineAreaStatuss);
                result.TotalCount  = mineAreaStatuss.TotalCount;
                result.CurrentPage = mineAreaStatuss.CurrentPage;
                result.PageSize    = mineAreaStatuss.PageSize;
                result.TotalPages  = mineAreaStatuss.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaStatusDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineAreaStatuss = await _mineAreaStatusRepository.GetByAccount(accountId, pageParams);
                if (mineAreaStatuss == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaStatusDto>>(mineAreaStatuss);
                result.TotalCount  = mineAreaStatuss.TotalCount;
                result.CurrentPage = mineAreaStatuss.CurrentPage;
                result.PageSize    = mineAreaStatuss.PageSize;
                result.TotalPages  = mineAreaStatuss.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineAreaStatusDto> GetById(int mineAreaStatusId) 
        {
            try
            {
                var mineAreaStatus = await _mineAreaStatusRepository.GetById(mineAreaStatusId);
                if (mineAreaStatus == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineAreaStatusDto>(mineAreaStatus);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}