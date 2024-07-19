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
    public class LithologyGroupSubController : ControllerBase
    {  
        private readonly ILithologyGroupSubService _lithologyGroupSubService;

        public LithologyGroupSubController(ILithologyGroupSubService lithologyGroupSubService)
        {
            _lithologyGroupSubService = lithologyGroupSubService;
        }
   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(LithologyGroupSubDto lithologyGroupSubDto)
        {
            try
            {
                var result = await _lithologyGroupSubService.Add(lithologyGroupSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add lithologyGroupSub. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(LithologyGroupSubDto lithologyGroupSubDto)
        {
            try
            {
                var result = await _lithologyGroupSubService.Update(lithologyGroupSubDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update lithologyGroupSub. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _lithologyGroupSubService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete lithologyGroupSub. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyGroupSubService.Get(pageParams);
                if(result == null) return NotFound("No lithologyGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyGroupSubService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No lithologyGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByLithologyGroup")]
        public async Task<IActionResult> GetByLithologyGroup(int groupId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyGroupSubService.GetByLithologyGroup(groupId, pageParams);
                if(result == null) return NotFound("No lithologyGroupSubs found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroupSubs. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _lithologyGroupSubService.GetById(id);
                if(result == null) return NotFound("No lithologyGroupSub found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroupSub. Error: {ex.Message}");
            }
        }

    }
}