using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DepositService: IDepositService
    {
        private readonly IDepositRepository _depositRepository;

        private readonly IMapper _mapper;
        
        public DepositService(IDepositRepository depositRepository,
                           IMapper mapper)
        {
            _depositRepository = depositRepository;
            _mapper = mapper;
        }

        public async Task<DepositDto> Add(DepositDto depositDto) 
        {
            try
            {
                //Map Dto > Class
                var addDeposit = _mapper.Map<Deposit>(depositDto); 
                //Add Deposit
                var resultCode = await _depositRepository.Add(addDeposit); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New Deposit
                var result = await _depositRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DepositDto>(result);
                return resultDto; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DepositDto> Update(DepositDto depositDto) 
        {
            try
            {
                //Check if exist Deposit
                var existDeposit = await _depositRepository.GetById(depositDto.Id);
                if (existDeposit == null) return null;
                //Map Dto > Class
                var updateDeposit = _mapper.Map<Deposit>(depositDto);
                //Update Deposit
                var resultCode = await _depositRepository.Update(updateDeposit); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated Deposit
                var result = await _depositRepository.GetById(updateDeposit.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DepositDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int depositId) 
        {
            try
            {
                return await _depositRepository.Delete(depositId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DepositDto>> Get(PageParams pageParams) 
        {
            try
            {
                var deposits = await _depositRepository.Get(pageParams);
                if (deposits == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DepositDto>>(deposits);
                result.TotalCount  = deposits.TotalCount;
                result.CurrentPage = deposits.CurrentPage;
                result.PageSize    = deposits.PageSize;
                result.TotalPages  = deposits.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DepositDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var deposits = await _depositRepository.GetByAccount(accountId, pageParams);
                if (deposits == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DepositDto>>(deposits);
                result.TotalCount  = deposits.TotalCount;
                result.CurrentPage = deposits.CurrentPage;
                result.PageSize    = deposits.PageSize;
                result.TotalPages  = deposits.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DepositDto>> GetByRegion(int regionId, PageParams pageParams) 
        {
            try
            {
                var deposits = await _depositRepository.GetByRegion(regionId, pageParams);
                if (deposits == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DepositDto>>(deposits);
                result.TotalCount  = deposits.TotalCount;
                result.CurrentPage = deposits.CurrentPage;
                result.PageSize    = deposits.PageSize;
                result.TotalPages  = deposits.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DepositDto> GetById(int depositId) 
        {
            try
            {
                var deposit = await _depositRepository.GetById(depositId);
                if (deposit == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DepositDto>(deposit);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}