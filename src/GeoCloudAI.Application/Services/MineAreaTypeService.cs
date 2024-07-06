using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class MineAreaTypeService: IMineAreaTypeService
    {
        private readonly IMineAreaTypeRepository _mineAreaTypeRepository;

        private readonly IMapper _mapper;
        
        public MineAreaTypeService(IMineAreaTypeRepository mineAreaTypeRepository,
                           IMapper mapper)
        {
            _mineAreaTypeRepository = mineAreaTypeRepository;
            _mapper = mapper;
        }

        public async Task<MineAreaTypeDto> Add(MineAreaTypeDto mineAreaTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineAreaType = _mapper.Map<MineAreaType>(mineAreaTypeDto); 
                //Add MineAreaType
                var resultCode = await _mineAreaTypeRepository.Add(addMineAreaType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineAreaType
                var result = await _mineAreaTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineAreaTypeDto> Update(MineAreaTypeDto mineAreaTypeDto) 
        {
            try
            {
                //Check if exist MineAreaType
                var existMineAreaType = await _mineAreaTypeRepository.GetById(mineAreaTypeDto.Id);
                if (existMineAreaType == null) return null;
                //Map Dto > Class
                var updateMineAreaType = _mapper.Map<MineAreaType>(mineAreaTypeDto);
                //Update MineAreaType
                var resultCode = await _mineAreaTypeRepository.Update(updateMineAreaType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineAreaType
                var result = await _mineAreaTypeRepository.GetById(updateMineAreaType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineAreaTypeId) 
        {
            try
            {
                return await _mineAreaTypeRepository.Delete(mineAreaTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineAreaTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineAreaTypes = await _mineAreaTypeRepository.Get(pageParams);
                if (mineAreaTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaTypeDto>>(mineAreaTypes);
                result.TotalCount  = mineAreaTypes.TotalCount;
                result.CurrentPage = mineAreaTypes.CurrentPage;
                result.PageSize    = mineAreaTypes.PageSize;
                result.TotalPages  = mineAreaTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineAreaTypes = await _mineAreaTypeRepository.GetByAccount(accountId, pageParams);
                if (mineAreaTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaTypeDto>>(mineAreaTypes);
                result.TotalCount  = mineAreaTypes.TotalCount;
                result.CurrentPage = mineAreaTypes.CurrentPage;
                result.PageSize    = mineAreaTypes.PageSize;
                result.TotalPages  = mineAreaTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineAreaTypeDto> GetById(int mineAreaTypeId) 
        {
            try
            {
                var mineAreaType = await _mineAreaTypeRepository.GetById(mineAreaTypeId);
                if (mineAreaType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineAreaTypeDto>(mineAreaType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}