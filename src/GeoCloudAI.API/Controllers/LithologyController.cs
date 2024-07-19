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
    public class LithologyController : ControllerBase
    {  
        private readonly ILithologyService _lithologyService;

        public LithologyController(ILithologyService lithologyService)
        {
            _lithologyService = lithologyService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(LithologyDto lithologyDto)
        {
            try
            {
                var result = await _lithologyService.Add(lithologyDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add lithology. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(LithologyDto lithologyDto)
        {
            try
            {
                var result = await _lithologyService.Update(lithologyDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update lithology. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _lithologyService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete lithology. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyService.Get(pageParams);
                if(result == null) return NotFound("No lithologys found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologys. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No lithologys found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologys. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByLithologyGroupSub")]
        public async Task<IActionResult> GetByLithologyGroupSub(int metalGroupId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyService.GetByLithologyGroupSub(metalGroupId, pageParams);
                if(result == null) return NotFound("No lithologys found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologys. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _lithologyService.GetById(id);
                if(result == null) return NotFound("No lithology found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithology. Error: {ex.Message}");
            }
        }

    }
}