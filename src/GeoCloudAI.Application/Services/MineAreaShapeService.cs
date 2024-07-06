using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class MineAreaShapeService: IMineAreaShapeService
    {
        private readonly IMineAreaShapeRepository _mineAreaShapeRepository;

        private readonly IMapper _mapper;
        
        public MineAreaShapeService(IMineAreaShapeRepository mineAreaShapeRepository,
                           IMapper mapper)
        {
            _mineAreaShapeRepository = mineAreaShapeRepository;
            _mapper = mapper;
        }

        public async Task<MineAreaShapeDto> Add(MineAreaShapeDto mineAreaShapeDto) 
        {
            try
            {
                //Map Dto > Class
                var addMineAreaShape = _mapper.Map<MineAreaShape>(mineAreaShapeDto); 
                //Add MineAreaShape
                var resultCode = await _mineAreaShapeRepository.Add(addMineAreaShape); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New MineAreaShape
                var result = await _mineAreaShapeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaShapeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<MineAreaShapeDto> Update(MineAreaShapeDto mineAreaShapeDto) 
        {
            try
            {
                //Check if exist MineAreaShape
                var existMineAreaShape = await _mineAreaShapeRepository.GetById(mineAreaShapeDto.Id);
                if (existMineAreaShape == null) return null;
                //Map Dto > Class
                var updateMineAreaShape = _mapper.Map<MineAreaShape>(mineAreaShapeDto);
                //Update MineAreaShape
                var resultCode = await _mineAreaShapeRepository.Update(updateMineAreaShape); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated MineAreaShape
                var result = await _mineAreaShapeRepository.GetById(updateMineAreaShape.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<MineAreaShapeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int mineAreaShapeId) 
        {
            try
            {
                return await _mineAreaShapeRepository.Delete(mineAreaShapeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<MineAreaShapeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var mineAreaShapes = await _mineAreaShapeRepository.Get(pageParams);
                if (mineAreaShapes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaShapeDto>>(mineAreaShapes);
                result.TotalCount  = mineAreaShapes.TotalCount;
                result.CurrentPage = mineAreaShapes.CurrentPage;
                result.PageSize    = mineAreaShapes.PageSize;
                result.TotalPages  = mineAreaShapes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<MineAreaShapeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var mineAreaShapes = await _mineAreaShapeRepository.GetByAccount(accountId, pageParams);
                if (mineAreaShapes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<MineAreaShapeDto>>(mineAreaShapes);
                result.TotalCount  = mineAreaShapes.TotalCount;
                result.CurrentPage = mineAreaShapes.CurrentPage;
                result.PageSize    = mineAreaShapes.PageSize;
                result.TotalPages  = mineAreaShapes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<MineAreaShapeDto> GetById(int mineAreaShapeId) 
        {
            try
            {
                var mineAreaShape = await _mineAreaShapeRepository.GetById(mineAreaShapeId);
                if (mineAreaShape == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<MineAreaShapeDto>(mineAreaShape);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}