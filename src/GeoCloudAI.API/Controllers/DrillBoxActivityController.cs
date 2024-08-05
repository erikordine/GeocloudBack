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
    public class DrillBoxActivityController : ControllerBase
    {  
        private readonly IDrillBoxActivityService _drillBoxActivityService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillBoxActivityController(IDrillBoxActivityService drillBoxActivityService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillBoxActivityService = drillBoxActivityService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillBoxActivityDto drillBoxActivityDto)
        {
            try
            {
                var result = await _drillBoxActivityService.Add(drillBoxActivityDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillBoxActivity. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(DrillBoxActivityDto drillBoxActivityDto)
        {
            try
            {
                var result = await _drillBoxActivityService.Update(drillBoxActivityDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillBoxActivity. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillBoxActivityService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillBoxActivity. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxActivityService.Get(pageParams);
                if(result == null) return NotFound("No drillBoxActivities found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxActivities. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxActivityService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No drillBoxActivities found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxActivities. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDrillBox")]
        public async Task<IActionResult> GetByDrillBox(int drillBoxId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxActivityService.GetByDrillBox(drillBoxId, pageParams);
                if(result == null) return NotFound("No drillBoxActivities found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxActivities. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillBoxActivityService.GetById(id);
                if(result == null) return NotFound("No drillBoxActivity found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxActivity. Error: {ex.Message}");
            }
        }

    }
}