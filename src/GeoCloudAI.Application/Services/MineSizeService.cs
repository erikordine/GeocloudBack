using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class MineSizeService: IMineSizeService
    {
        private readonly IMineSizeRepository _mineSizeRepository;

        private readonly IMapper _mapper;
        
        public MineSizeService(IMineSizeRepository mineSizeRepository,
                           IMapper mapper)
        {
            _mineSizeRepository = mineSizeRepository;
            _mapper = mapper;
        }

        public async Task<MineSizeDto> Add(MineSizeDto mineSizeDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineSize = _mapper.Map<MineSize>(mineSizeDto); 
                //Add MineSize
                var resultCode = await _mineSizeRepository.Add(addMineSize); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineSize
                var result = await _mineSizeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineSizeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineSizeDto> Update(MineSizeDto mineSizeDto) 
        {
            try
            {
                //Check if exist MineSize
                var existMineSize = await _mineSizeRepository.GetById(mineSizeDto.Id);
                if (existMineSize == null) return null;
                //Map Dto > Class
                var updateMineSize = _mapper.Map<MineSize>(mineSizeDto);
                //Update MineSize
                var resultCode = await _mineSizeRepository.Update(updateMineSize); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineSize
                var result = await _mineSizeRepository.GetById(updateMineSize.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineSizeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineSizeId) 
        {
            try
            {
                return await _mineSizeRepository.Delete(mineSizeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineSizeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineSizes = await _mineSizeRepository.Get(pageParams);
                if (mineSizes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineSizeDto>>(mineSizes);
                result.TotalCount  = mineSizes.TotalCount;
                result.CurrentPage = mineSizes.CurrentPage;
                result.PageSize    = mineSizes.PageSize;
                result.TotalPages  = mineSizes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineSizeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineSizes = await _mineSizeRepository.GetByAccount(accountId, pageParams);
                if (mineSizes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineSizeDto>>(mineSizes);
                result.TotalCount  = mineSizes.TotalCount;
                result.CurrentPage = mineSizes.CurrentPage;
                result.PageSize    = mineSizes.PageSize;
                result.TotalPages  = mineSizes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineSizeDto> GetById(int mineSizeId) 
        {
            try
            {
                var mineSize = await _mineSizeRepository.GetById(mineSizeId);
                if (mineSize == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineSizeDto>(mineSize);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}