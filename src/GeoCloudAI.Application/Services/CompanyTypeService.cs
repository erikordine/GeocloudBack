using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class CompanyTypeService: ICompanyTypeService
    {
        private readonly ICompanyTypeRepository _companyTypeRepository;

        private readonly IMapper _mapper;
        
        public CompanyTypeService(ICompanyTypeRepository companyTypeRepository,
                           IMapper mapper)
        {
            _companyTypeRepository = companyTypeRepository;
            _mapper = mapper;
        }

        public async Task<CompanyTypeDto> Add(CompanyTypeDto companyTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addCompanyType = _mapper.Map<Domain.Classes.CompanyType>(companyTypeDto); 
                //Add CompanyType
                var resultCode = await _companyTypeRepository.Add(addCompanyType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New CompanyType
                var result = await _companyTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CompanyTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<CompanyTypeDto> Update(CompanyTypeDto companyTypeDto) 
        {
            try
            {
                //Check if exist CompanyType
                var existCompanyType = await _companyTypeRepository.GetById(companyTypeDto.Id);
                if (existCompanyType == null) return null;
                //Map Dto > Class
                var updateCompanyType = _mapper.Map<Domain.Classes.CompanyType>(companyTypeDto);
                //Update CompanyType
                var resultCode = await _companyTypeRepository.Update(updateCompanyType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated CompanyType
                var result = await _companyTypeRepository.GetById(updateCompanyType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CompanyTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int companyTypeId) 
        {
            try
            {
                return await _companyTypeRepository.Delete(companyTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<CompanyTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var companyTypes = await _companyTypeRepository.Get(pageParams);
                if (companyTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CompanyTypeDto>>(companyTypes);
                result.TotalCount  = companyTypes.TotalCount;
                result.CurrentPage = companyTypes.CurrentPage;
                result.PageSize    = companyTypes.PageSize;
                result.TotalPages  = companyTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<CompanyTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var companyTypes = await _companyTypeRepository.GetByAccount(accountId, pageParams);
                if (companyTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CompanyTypeDto>>(companyTypes);
                result.TotalCount  = companyTypes.TotalCount;
                result.CurrentPage = companyTypes.CurrentPage;
                result.PageSize    = companyTypes.PageSize;
                result.TotalPages  = companyTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<CompanyTypeDto> GetById(int companyTypeId) 
        {
            try
            {
                var companyType = await _companyTypeRepository.GetById(companyTypeId);
                if (companyType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<CompanyTypeDto>(companyType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}