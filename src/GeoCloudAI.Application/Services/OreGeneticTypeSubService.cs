using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class OreGeneticTypeSubService: IOreGeneticTypeSubService
    {
        private readonly IOreGeneticTypeSubRepository _oreGeneticTypeSubRepository;

        private readonly IMapper _mapper;
        
        public OreGeneticTypeSubService(IOreGeneticTypeSubRepository oreGeneticTypeSubRepository,
                           IMapper mapper)
        {
            _oreGeneticTypeSubRepository = oreGeneticTypeSubRepository;
            _mapper = mapper;
        }

        public async Task<OreGeneticTypeSubDto> Add(OreGeneticTypeSubDto oreGeneticTypeSubDto) 
        {
            try
            {
                //Map Dto > Class
                var addOreGeneticTypeSub = _mapper.Map<Domain.Classes.OreGeneticTypeSub>(oreGeneticTypeSubDto); 
                //Add OreGeneticTypeSub
                var resultCode = await _oreGeneticTypeSubRepository.Add(addOreGeneticTypeSub); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New OreGeneticTypeSub
                var result = await _oreGeneticTypeSubRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<OreGeneticTypeSubDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<OreGeneticTypeSubDto> Update(OreGeneticTypeSubDto oreGeneticTypeSubDto) 
        {
            try
            {
                //Check if exist OreGeneticTypeSub
                var existOreGeneticTypeSub = await _oreGeneticTypeSubRepository.GetById(oreGeneticTypeSubDto.Id);
                if (existOreGeneticTypeSub == null) return null;
                //Map Dto > Class
                var updateOreGeneticTypeSub = _mapper.Map<Domain.Classes.OreGeneticTypeSub>(oreGeneticTypeSubDto);
                //Update OreGeneticTypeSub
                var resultCode = await _oreGeneticTypeSubRepository.Update(updateOreGeneticTypeSub); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated OreGeneticTypeSub
                var result = await _oreGeneticTypeSubRepository.GetById(updateOreGeneticTypeSub.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<OreGeneticTypeSubDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int oreGeneticTypeSubId) 
        {
            try
            {
                return await _oreGeneticTypeSubRepository.Delete(oreGeneticTypeSubId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<OreGeneticTypeSubDto>> Get(PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypeSubs = await _oreGeneticTypeSubRepository.Get(pageParams);
                if (oreGeneticTypeSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeSubDto>>(oreGeneticTypeSubs);
                result.TotalCount  = oreGeneticTypeSubs.TotalCount;
                result.CurrentPage = oreGeneticTypeSubs.CurrentPage;
                result.PageSize    = oreGeneticTypeSubs.PageSize;
                result.TotalPages  = oreGeneticTypeSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<OreGeneticTypeSubDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypeSubs = await _oreGeneticTypeSubRepository.GetByAccount(accountId, pageParams);
                if (oreGeneticTypeSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeSubDto>>(oreGeneticTypeSubs);
                result.TotalCount  = oreGeneticTypeSubs.TotalCount;
                result.CurrentPage = oreGeneticTypeSubs.CurrentPage;
                result.PageSize    = oreGeneticTypeSubs.PageSize;
                result.TotalPages  = oreGeneticTypeSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<OreGeneticTypeSubDto>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypeSubs = await _oreGeneticTypeSubRepository.GetByOreGeneticType(oreGeneticTypeId, pageParams);
                if (oreGeneticTypeSubs == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeSubDto>>(oreGeneticTypeSubs);
                result.TotalCount  = oreGeneticTypeSubs.TotalCount;
                result.CurrentPage = oreGeneticTypeSubs.CurrentPage;
                result.PageSize    = oreGeneticTypeSubs.PageSize;
                result.TotalPages  = oreGeneticTypeSubs.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<OreGeneticTypeSubDto> GetById(int oreGeneticTypeSubId) 
        {
            try
            {
                var oreGeneticTypeSub = await _oreGeneticTypeSubRepository.GetById(oreGeneticTypeSubId);
                if (oreGeneticTypeSub == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<OreGeneticTypeSubDto>(oreGeneticTypeSub);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}