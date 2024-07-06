using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IMapper _mapper;
        
        public CompanyService(ICompanyRepository companyRepository,
                           IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Add(CompanyDto companyDto) 
        {
            try
            {
                //Map Dto > Class
                var addCompany = _mapper.Map<Company>(companyDto); 
                //Add Company
                var resultCode = await _companyRepository.Add(addCompany); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Company
                var result = await _companyRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CompanyDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<CompanyDto> Update(CompanyDto companyDto) 
        {
            try
            {
                //Check if exist Company
                var existCompany = await _companyRepository.GetById(companyDto.Id);
                if (existCompany == null) return null;
                //Map Dto > Class
                var updateCompany = _mapper.Map<Company>(companyDto);
                //Update Company
                var resultCode = await _companyRepository.Update(updateCompany); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Company
                var result = await _companyRepository.GetById(updateCompany.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<CompanyDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int companyId) 
        {
            try
            {
                return await _companyRepository.Delete(companyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<CompanyDto>> Get(PageParams pageParams) 
        {
            try
            {
                var companys = await _companyRepository.Get(pageParams);
                if (companys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CompanyDto>>(companys);
                result.TotalCount  = companys.TotalCount;
                result.CurrentPage = companys.CurrentPage;
                result.PageSize    = companys.PageSize;
                result.TotalPages  = companys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<CompanyDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var companys = await _companyRepository.GetByAccount(accountId, pageParams);
                if (companys == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<CompanyDto>>(companys);
                result.TotalCount  = companys.TotalCount;
                result.CurrentPage = companys.CurrentPage;
                result.PageSize    = companys.PageSize;
                result.TotalPages  = companys.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<CompanyDto> GetById(int companyId) 
        {
            try
            {
                var company = await _companyRepository.GetById(companyId);
                if (company == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<CompanyDto>(company);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}