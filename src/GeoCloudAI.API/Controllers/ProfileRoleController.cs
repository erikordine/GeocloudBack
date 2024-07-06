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
    public class ProfileRoleController : ControllerBase
    {  
        private readonly IProfileRoleService _profileRoleService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileRoleController(IProfileRoleService profileRoleService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _profileRoleService = profileRoleService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(ProfileRoleDto profileRoleDto)
        {
            try
            {
                var result = await _profileRoleService.Add(profileRoleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add profileRole. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _profileRoleService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete profileRole. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _profileRoleService.Get(pageParams);
                if(result == null) return NotFound("No profileRoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profileRoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _profileRoleService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No profileRoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profileRoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByProfile")]
        public async Task<IActionResult> GetByProfile(int profileId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _profileRoleService.GetByProfile(profileId, pageParams);
                if(result == null) return NotFound("No profileRoles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profileRoles. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _profileRoleService.GetById(id);
                if(result == null) return NotFound("No profileRole found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profileRole. Error: {ex.Message}");
            }
        }

    }
}