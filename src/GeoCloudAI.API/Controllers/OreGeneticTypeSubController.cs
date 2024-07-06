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
    public class OreGeneticTypeSubController : ControllerBase
    {  
        private readonly IOreGeneticTypeSubService _oreGeneticTypeSubService;

        public OreGeneticTypeSubController(IOreGeneticTypeSubService oreGeneticTypeSubService)
        {
            _oreGeneticTypeSubService = oreGeneticTypeSubService;
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(OreGeneticTypeSubDto oreGeneticTypeSubDto)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.Add(oreGeneticTypeSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add oreGeneticTypeSub. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(OreGeneticTypeSubDto oreGeneticTypeSubDto)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.Update(oreGeneticTypeSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update oreGeneticTypeSub. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete oreGeneticTypeSub. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.Get(pageParams);
                if(result == null) return NotFound("No oreGeneticTypeSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypeSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No oreGeneticTypeSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypeSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByOreGeneticType")]
        public async Task<IActionResult> GetByOreGeneticType(int oreGeneticTypeId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.GetByOreGeneticType(oreGeneticTypeId, pageParams);
                if(result == null) return NotFound("No oreGeneticTypeSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypeSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _oreGeneticTypeSubService.GetById(id);
                if(result == null) return NotFound("No oreGeneticTypeSub found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover oreGeneticTypeSub. Error: {ex.Message}");
            }
        }

    }
}