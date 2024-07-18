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
    public class DrillHoleRunController : ControllerBase
    {  
        private readonly IDrillHoleRunService _drillHoleRunService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillHoleRunController(IDrillHoleRunService drillHoleRunService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillHoleRunService = drillHoleRunService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillHoleRunDto drillHoleRunDto)
        {
            try
            {
                var result = await _drillHoleRunService.Add(drillHoleRunDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillHoleRun. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(DrillHoleRunDto drillHoleRunDto)
        {
            try
            {
                var result = await _drillHoleRunService.Update(drillHoleRunDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillHoleRun. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillHoleRunService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillHoleRun. Error: {ex.Message}");
            }
        }
    
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleRunService.Get(pageParams);
                if(result == null) return NotFound("No drillHoleRunes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleRunes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDrillHole")]
        public async Task<IActionResult> GetByDrillHole(int drillHoleId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleRunService.GetByDrillHole(drillHoleId, pageParams);
                if(result == null) return NotFound("No drillHoleRunes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleRunes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillHoleRunService.GetById(id);
                if(result == null) return NotFound("No drillHoleRun found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleRun. Error: {ex.Message}");
            }
        }
    }
}