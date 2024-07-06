using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class RegionService: IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        private readonly IMapper _mapper;
        
        public RegionService(IRegionRepository regionRepository,
                           IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<RegionDto> Add(RegionDto regionDto) 
        {
            try
            {
                //Map Dto > Class
                var addRegion = _mapper.Map<Region>(regionDto); 
                //Add Region
                var resultCode = await _regionRepository.Add(addRegion); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Region
                var result = await _regionRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<RegionDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<RegionDto> Update(RegionDto regionDto) 
        {
            try
            {
                //Check if exist Region
                var existRegion = await _regionRepository.GetById(regionDto.Id);
                if (existRegion == null) return null;
                //Map Dto > Class
                var updateRegion = _mapper.Map<Region>(regionDto);
                //Update Region
                var resultCode = await _regionRepository.Update(updateRegion); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Region
                var result = await _regionRepository.GetById(updateRegion.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<RegionDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int regionId) 
        {
            try
            {
                return await _regionRepository.Delete(regionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<RegionDto>> Get(PageParams pageParams) 
        {
            try
            {
                var regions = await _regionRepository.Get(pageParams);
                if (regions == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<RegionDto>>(regions);
                result.TotalCount  = regions.TotalCount;
                result.CurrentPage = regions.CurrentPage;
                result.PageSize    = regions.PageSize;
                result.TotalPages  = regions.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<RegionDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var regions = await _regionRepository.GetByAccount(accountId, pageParams);
                if (regions == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<RegionDto>>(regions);
                result.TotalCount  = regions.TotalCount;
                result.CurrentPage = regions.CurrentPage;
                result.PageSize    = regions.PageSize;
                result.TotalPages  = regions.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<RegionDto> GetById(int regionId) 
        {
            try
            {
                var region = await _regionRepository.GetById(regionId);
                if (region == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<RegionDto>(region);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}