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
    public class MetalGroupSubController : ControllerBase
    {  
        private readonly IMetalGroupSubService _metalGroupSubService;

        public MetalGroupSubController(IMetalGroupSubService metalGroupSubService)
        {
            _metalGroupSubService = metalGroupSubService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(MetalGroupSubDto metalGroupSubDto)
        {
            try
            {
                var result = await _metalGroupSubService.Add(metalGroupSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add metalGroupSub. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(MetalGroupSubDto metalGroupSubDto)
        {
            try
            {
                var result = await _metalGroupSubService.Update(metalGroupSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update metalGroupSub. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _metalGroupSubService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete metalGroupSub. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupSubService.Get(pageParams);
                if(result == null) return NotFound("No metalGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupSubService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No metalGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMetalGroup")]
        public async Task<IActionResult> GetByMetalGroup(int metalGroupId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _metalGroupSubService.GetByMetalGroup(metalGroupId, pageParams);
                if(result == null) return NotFound("No metalGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _metalGroupSubService.GetById(id);
                if(result == null) return NotFound("No metalGroupSub found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover metalGroupSub. Error: {ex.Message}");
            }
        }

    }
}