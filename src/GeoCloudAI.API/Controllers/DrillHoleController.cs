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
    public class DrillHoleController : ControllerBase
    {  
        private readonly IDrillHoleService _drillHoleService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillHoleController(IDrillHoleService drillHoleService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillHoleService = drillHoleService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillHoleDto drillHoleDto)
        {
            try
            {
                var result = await _drillHoleService.Add(drillHoleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillHole. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(DrillHoleDto drillHoleDto)
        {
            try
            {
                var result = await _drillHoleService.Update(drillHoleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillHole. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillHoleService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillHole. Error: {ex.Message}");
            }
        }
    
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.Get(pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByRegion")]
        public async Task<IActionResult> GetByRegion(int regionId, bool direct, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.GetByRegion(regionId, direct, pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDeposit")]
        public async Task<IActionResult> GetByDeposit(int depositId, bool direct, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.GetByDeposit(depositId, direct, pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMine")]
        public async Task<IActionResult> GetByMine(int mineId, bool direct, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.GetByMine(mineId, direct, pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMineArea")]
        public async Task<IActionResult> GetByMineArea(int mineAreaId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillHoleService.GetByMineArea(mineAreaId, pageParams);
                if(result == null) return NotFound("No drillHoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillHoleService.GetById(id);
                if(result == null) return NotFound("No drillHole found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillHole. Error: {ex.Message}");
            }
        }

    }
}