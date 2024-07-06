using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Services
{
    public class DepositTypeService: IDepositTypeService
    {
        private readonly IDepositTypeRepository _depositTypeRepository;

        private readonly IMapper _mapper;
        
        public DepositTypeService(IDepositTypeRepository depositTypeRepository,
                           IMapper mapper)
        {
            _depositTypeRepository = depositTypeRepository;
            _mapper = mapper;
        }

        public async Task<DepositTypeDto> Add(DepositTypeDto depositTypeDto) 
        {
            try
            {
                //Map Dto > Class
                var addDepositType = _mapper.Map<Domain.Classes.DepositType>(depositTypeDto); 
                //Add DepositType
                var resultCode = await _depositTypeRepository.Add(addDepositType); // resultCode = "0" or "new Id"
                if (resultCode == 0) return null;
                //Get New DepositType
                var result = await _depositTypeRepository.GetById(resultCode);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DepositTypeDto>(result);
                return resultDto;       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<DepositTypeDto> Update(DepositTypeDto depositTypeDto) 
        {
            try
            {
                //Check if exist DepositType
                var existDepositType = await _depositTypeRepository.GetById(depositTypeDto.Id);
                if (existDepositType == null) return null;
                //Map Dto > Class
                var updateDepositType = _mapper.Map<Domain.Classes.DepositType>(depositTypeDto);
                //Update DepositType
                var resultCode = await _depositTypeRepository.Update(updateDepositType); // resultCode = "0" or "1"
                if (resultCode == 0) return null;
                //Get Updated DepositType
                var result = await _depositTypeRepository.GetById(updateDepositType.Id);
                if (result == null) return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<DepositTypeDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<int> Delete(int depositTypeId) 
        {
            try
            {
                return await _depositTypeRepository.Delete(depositTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<PageList<DepositTypeDto>> Get(PageParams pageParams) 
        {
            try
            {
                var depositTypes = await _depositTypeRepository.Get(pageParams);
                if (depositTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DepositTypeDto>>(depositTypes);
                result.TotalCount  = depositTypes.TotalCount;
                result.CurrentPage = depositTypes.CurrentPage;
                result.PageSize    = depositTypes.PageSize;
                result.TotalPages  = depositTypes.TotalPages;

                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<PageList<DepositTypeDto>> GetByAccount(int accountId, PageParams pageParams) 
        {
            try
            {
                var depositTypes = await _depositTypeRepository.GetByAccount(accountId, pageParams);
                if (depositTypes == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<PageList<DepositTypeDto>>(depositTypes);
                result.TotalCount  = depositTypes.TotalCount;
                result.CurrentPage = depositTypes.CurrentPage;
                result.PageSize    = depositTypes.PageSize;
                result.TotalPages  = depositTypes.TotalPages;
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<DepositTypeDto> GetById(int depositTypeId) 
        {
            try
            {
                var depositType = await _depositTypeRepository.GetById(depositTypeId);
                if (depositType == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<DepositTypeDto>(depositType);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}