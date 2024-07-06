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
    public class MineAreaController : ControllerBase
    {  
        private readonly IMineAreaService _mineAreaService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public MineAreaController(IMineAreaService mineAreaService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _mineAreaService = mineAreaService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(MineAreaDto mineAreaDto)
        {
            try
            {
                var result = await _mineAreaService.Add(mineAreaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add mineArea. Error: {ex.Message}");
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
        public async Task<IActionResult> Update(MineAreaDto mineAreaDto)
        {
            try
            {
                var result = await _mineAreaService.Update(mineAreaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update mineArea. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mineAreaService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete mineArea. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _mineAreaService.Get(pageParams);
                if(result == null) return NotFound("No mineAreas found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineAreas. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _mineAreaService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No mineAreas found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineAreas. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByRegion")]
        public async Task<IActionResult> GetByRegion(int regionId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _mineAreaService.GetByRegion(regionId, pageParams);
                if(result == null) return NotFound("No mineAreas found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineAreas. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDeposit")]
        public async Task<IActionResult> GetByDeposit(int depositId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _mineAreaService.GetByDeposit(depositId, pageParams);
                if(result == null) return NotFound("No mineAreas found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineAreas. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMine")]
        public async Task<IActionResult> GetByMine(int mineId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _mineAreaService.GetByMine(mineId, pageParams);
                if(result == null) return NotFound("No mineAreas found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineAreas. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mineAreaService.GetById(id);
                if(result == null) return NotFound("No mineArea found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover mineArea. Error: {ex.Message}");
            }
        }

    }
}