using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LithologyGroupController : ControllerBase
    {  
        private readonly ILithologyGroupService _lithologyGroupService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public LithologyGroupController(ILithologyGroupService lithologyGroupService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _lithologyGroupService = lithologyGroupService;
        }
   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(LithologyGroupDto lithologyGroupDto)
        {
            try
            {
                var result = await _lithologyGroupService.Add(lithologyGroupDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add lithologyGroup. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(LithologyGroupDto lithologyGroupDto)
        {
            try
            {
                var result = await _lithologyGroupService.Update(lithologyGroupDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update lithologyGroup. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _lithologyGroupService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete lithologyGroup. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyGroupService.Get(pageParams);
                if(result == null) return NotFound("No lithologyGroups found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroups. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyGroupService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No lithologyGroups found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroups. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _lithologyGroupService.GetById(id);
                if(result == null) return NotFound("No lithologyGroup found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyGroup. Error: {ex.Message}");
            }
        }
    }
}