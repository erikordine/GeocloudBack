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
    public class FunctionalityTypeController : ControllerBase
    {  
        private readonly IFunctionalityTypeService _functionalityTypeService;

        public FunctionalityTypeController(IFunctionalityTypeService functionalityTypeService)
        {
            _functionalityTypeService = functionalityTypeService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(FunctionalityTypeDto functionalityTypeDto)
        {
            try
            {
                var result = await _functionalityTypeService.Add(functionalityTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add functionalityType. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(FunctionalityTypeDto functionalityTypeDto)
        {
            try
            {
                var result = await _functionalityTypeService.Update(functionalityTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update functionalityType. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _functionalityTypeService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete functionalityType. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _functionalityTypeService.Get(pageParams);
                if(result == null) return NotFound("No functionalityTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover functionalityTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _functionalityTypeService.GetById(id);
                if(result == null) return NotFound("No functionalityType found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover functionalityType. Error: {ex.Message}");
            }
        }

    }
}