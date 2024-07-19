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
    public class LithologyMethodController : ControllerBase
    {  
        private readonly ILithologyMethodService _lithologyMethodService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public LithologyMethodController(ILithologyMethodService lithologyMethodService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _lithologyMethodService = lithologyMethodService;
        }
   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(LithologyMethodDto lithologyMethodDto)
        {
            try
            {
                var result = await _lithologyMethodService.Add(lithologyMethodDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add lithologyMethod. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(LithologyMethodDto lithologyMethodDto)
        {
            try
            {
                var result = await _lithologyMethodService.Update(lithologyMethodDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update lithologyMethod. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _lithologyMethodService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete lithologyMethod. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyMethodService.Get(pageParams);
                if(result == null) return NotFound("No lithologyMethods found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyMethods. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _lithologyMethodService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No lithologyMethods found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyMethods. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _lithologyMethodService.GetById(id);
                if(result == null) return NotFound("No lithologyMethod found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover lithologyMethod. Error: {ex.Message}");
            }
        }
    }
}