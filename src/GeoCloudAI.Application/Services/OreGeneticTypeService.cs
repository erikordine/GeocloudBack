using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class OreGeneticTypeService: IOreGeneticTypeService
    {
        private readonly IOreGeneticTypeRepository _oreGeneticTypeRepository;

        private readonly IMapper _mapper;
        
        public OreGeneticTypeService(IOreGeneticTypeRepository oreGeneticTypeRepository,
                                     IMapper mapper)
        {
            _oreGeneticTypeRepository = oreGeneticTypeRepository;
            _mapper = mapper;
        }

        public async Task<OreGeneticTypeDto> Add(OreGeneticTypeDto oreGeneticTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addOreGeneticType = _mapper.Map<Domain.Classes.OreGeneticType>(oreGeneticTypeDto); 
                //Add OreGeneticType
                var resultCode = await _oreGeneticTypeRepository.Add(addOreGeneticType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New OreGeneticType
                var result = await _oreGeneticTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<OreGeneticTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<OreGeneticTypeDto> Update(OreGeneticTypeDto oreGeneticTypeDto) 
        {
            try
            {
                //Check if exist OreGeneticType
                var existOreGeneticType = await _oreGeneticTypeRepository.GetById(oreGeneticTypeDto.Id);
                if (existOreGeneticType == null) return null;
                //Map Dto > Class
                var updateOreGeneticType = _mapper.Map<Domain.Classes.OreGeneticType>(oreGeneticTypeDto);
                //Update OreGeneticType
                var resultCode = await _oreGeneticTypeRepository.Update(updateOreGeneticType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated OreGeneticType
                var result = await _oreGeneticTypeRepository.GetById(updateOreGeneticType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<OreGeneticTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int oreGeneticTypeId) 
        {
            try
            {
                return await _oreGeneticTypeRepository.Delete(oreGeneticTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<OreGeneticTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypes = await _oreGeneticTypeRepository.Get(pageParams);
                if (oreGeneticTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeDto>>(oreGeneticTypes);
                result.TotalCount  = oreGeneticTypes.TotalCount;
                result.CurrentPage = oreGeneticTypes.CurrentPage;
                result.PageSize    = oreGeneticTypes.PageSize;
                result.TotalPages  = oreGeneticTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<OreGeneticTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypes = await _oreGeneticTypeRepository.GetByAccount(accountId, pageParams);
                if (oreGeneticTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeDto>>(oreGeneticTypes);
                result.TotalCount  = oreGeneticTypes.TotalCount;
                result.CurrentPage = oreGeneticTypes.CurrentPage;
                result.PageSize    = oreGeneticTypes.PageSize;
                result.TotalPages  = oreGeneticTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<OreGeneticTypeDto>> GetByDepositType(int depositTypeId, PageParams pageParams) 
        {
            try
            {
                var oreGeneticTypes = await _oreGeneticTypeRepository.GetByDepositType(depositTypeId, pageParams);
                if (oreGeneticTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<OreGeneticTypeDto>>(oreGeneticTypes);
                result.TotalCount  = oreGeneticTypes.TotalCount;
                result.CurrentPage = oreGeneticTypes.CurrentPage;
                result.PageSize    = oreGeneticTypes.PageSize;
                result.TotalPages  = oreGeneticTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<OreGeneticTypeDto> GetById(int oreGeneticTypeId) 
        {
            try
            {
                var oreGeneticType = await _oreGeneticTypeRepository.GetById(oreGeneticTypeId);
                if (oreGeneticType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<OreGeneticTypeDto>(oreGeneticType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}