using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Application.Services
{
    public class DrillBoxMaterialService: IDrillBoxMaterialService
    {
        private readonly IDrillBoxMaterialRepository _drillBoxMaterialRepository;

        private readonly IMapper _mapper;
        
        public DrillBoxMaterialService(IDrillBoxMaterialRepository drillBoxMaterialRepository,
                           IMapper mapper)
        {
            _drillBoxMaterialRepository = drillBoxMaterialRepository;
            _mapper = mapper;
        }

        public async Task<DrillBoxMaterialDto> Add(DrillBoxMaterialDto drillBoxMaterialDto) 
        {
            try
            {
                //Map Dto > Class
                var addDrillBoxMaterial = _mapper.Map<DrillBoxMaterial>(drillBoxMaterialDto); 
                //Add DrillBoxMaterial
                var resultCode = await _drillBoxMaterialRepository.Add(addDrillBoxMaterial); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DrillBoxMaterial
                var result = await _drillBoxMaterialRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxMaterialDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DrillBoxMaterialDto> Update(DrillBoxMaterialDto drillBoxMaterialDto) 
        {
            try
            {
                //Check if exist DrillBoxMaterial
                var existDrillBoxMaterial = await _drillBoxMaterialRepository.GetById(drillBoxMaterialDto.Id);
                if (existDrillBoxMaterial == null) return null;
                //Map Dto > Class
                var updateDrillBoxMaterial = _mapper.Map<DrillBoxMaterial>(drillBoxMaterialDto);
                //Update DrillBoxMaterial
                var resultCode = await _drillBoxMaterialRepository.Update(updateDrillBoxMaterial); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DrillBoxMaterial
                var result = await _drillBoxMaterialRepository.GetById(updateDrillBoxMaterial.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DrillBoxMaterialDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int drillBoxMaterialId) 
        {
            try
            {
                return await _drillBoxMaterialRepository.Delete(drillBoxMaterialId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DrillBoxMaterialDto>> Get(PageParams pageParams) 
        {
            try
            {
                var drillBoxMaterials = await _drillBoxMaterialRepository.Get(pageParams);
                if (drillBoxMaterials == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxMaterialDto>>(drillBoxMaterials);
                result.TotalCount  = drillBoxMaterials.TotalCount;
                result.CurrentPage = drillBoxMaterials.CurrentPage;
                result.PageSize    = drillBoxMaterials.PageSize;
                result.TotalPages  = drillBoxMaterials.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DrillBoxMaterialDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var drillBoxMaterials = await _drillBoxMaterialRepository.GetByAccount(accountId, pageParams);
                if (drillBoxMaterials == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DrillBoxMaterialDto>>(drillBoxMaterials);
                result.TotalCount  = drillBoxMaterials.TotalCount;
                result.CurrentPage = drillBoxMaterials.CurrentPage;
                result.PageSize    = drillBoxMaterials.PageSize;
                result.TotalPages  = drillBoxMaterials.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DrillBoxMaterialDto> GetById(int drillBoxMaterialId) 
        {
            try
            {
                var drillBoxMaterial = await _drillBoxMaterialRepository.GetById(drillBoxMaterialId);
                if (drillBoxMaterial == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DrillBoxMaterialDto>(drillBoxMaterial);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        
    }
}