using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OreGeneticTypeController : ControllerBase
    {  
        private readonly IOreGeneticTypeService _oreGeneticTypeService;

        public OreGeneticTypeController(IOreGeneticTypeService oreGeneticTypeService)
        {
            _oreGeneticTypeService = oreGeneticTypeService;
        }
   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(OreGeneticTypeDto oreGeneticTypeDto)
        {
            try
            {
                var result = await _oreGeneticTypeService.Add(oreGeneticTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add oreGeneticType. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(OreGeneticTypeDto oreGeneticTypeDto)
        {
            try
            {
                var result = await _oreGeneticTypeService.Update(oreGeneticTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update oreGeneticType. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _oreGeneticTypeService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete oreGeneticType. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeService.Get(pageParams);
                if(result == null) return NotFound("No oreGeneticTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No oreGeneticTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDepositType")]
        public async Task<IActionResult> GetByDepositType(int depositTypeId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeService.GetByDepositType(depositTypeId, pageParams);
                if(result == null) return NotFound("No oreGeneticTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _oreGeneticTypeService.GetById(id);
                if(result == null) return NotFound("No oreGeneticType found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticType. Error: {ex.Message}");
            }
        }

    }
}