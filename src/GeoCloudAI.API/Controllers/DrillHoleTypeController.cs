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
    public class DrillHoleTypeController : ControllerBase
    {  
        private readonly IDrillHoleTypeService _drillHoleTypeService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillHoleTypeController(IDrillHoleTypeService drillHoleTypeService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillHoleTypeService = drillHoleTypeService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillHoleTypeDto drillHoleTypeDto)
        {
            try
            {
                var result = await _drillHoleTypeService.Add(drillHoleTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillHoleType. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(DrillHoleTypeDto drillHoleTypeDto)
        {
            try
            {
                var result = await _drillHoleTypeService.Update(drillHoleTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillHoleType. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillHoleTypeService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillHoleType. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleTypeService.Get(pageParams);
                if(result == null) return NotFound("No drillHoleTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleTypeService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No drillHoleTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillHoleTypeService.GetById(id);
                if(result == null) return NotFound("No drillHoleType found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoleType. Error: {ex.Message}");
            }
        }

    }
}