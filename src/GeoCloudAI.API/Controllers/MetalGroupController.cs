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
    public class MetalGroupController : ControllerBase
    {  
        private readonly IMetalGroupService _metalGroupService;

        public MetalGroupController(IMetalGroupService metalGroupService)
        {
            _metalGroupService = metalGroupService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(MetalGroupDto metalGroupDto)
        {
            try
            {
                var result = await _metalGroupService.Add(metalGroupDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add metalGroup. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(MetalGroupDto metalGroupDto)
        {
            try
            {
                var result = await _metalGroupService.Update(metalGroupDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update metalGroup. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _metalGroupService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete metalGroup. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupService.Get(pageParams);
                if(result == null) return NotFound("No metalGroups found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroups. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No metalGroups found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroups. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByOreGeneticType")]
        public async Task<IActionResult> GetByOreGeneticType(int oreGeneticTypeId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupService.GetByOreGeneticType(oreGeneticTypeId, pageParams);
                if(result == null) return NotFound("No metalGroups found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroups. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _metalGroupService.GetById(id);
                if(result == null) return NotFound("No metalGroup found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroup. Error: {ex.Message}");
            }
        }

    }
}