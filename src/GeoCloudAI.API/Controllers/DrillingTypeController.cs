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
    public class DrillingTypeController : ControllerBase
    {  
        private readonly IDrillingTypeService _drillingTypeService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillingTypeController(IDrillingTypeService drillingTypeService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillingTypeService = drillingTypeService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillingTypeDto drillingTypeDto)
        {
            try
            {
                var result = await _drillingTypeService.Add(drillingTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillingType. Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("uploadImage")]
        public async Task<IActionResult> UploadImage(string pathName)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0) {
                    var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, pathName);
                    //Create directory (if necessary)
                    FileInfo finfo = new FileInfo(pathName);
                    if (!Directory.Exists(finfo.DirectoryName)) {
                        Directory.CreateDirectory(finfo.DirectoryName!);
                    };
                    using ( var fileStream = new FileStream(imagePath, FileMode.Create)) {
                        await file.CopyToAsync(fileStream);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to upload image. Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(DrillingTypeDto drillingTypeDto)
        {
            try
            {
                var result = await _drillingTypeService.Update(drillingTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillingType. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillingTypeService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillingType. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillingTypeService.Get(pageParams);
                if(result == null) return NotFound("No drillingTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillingTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillingTypeService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No drillingTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillingTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillingTypeService.GetById(id);
                if(result == null) return NotFound("No drillingType found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillingType. Error: {ex.Message}");
            }
        }

    }
}