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
    public class DrillBoxController : ControllerBase
    {  
        private readonly IDrillBoxService _drillBoxService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public DrillBoxController(IDrillBoxService drillBoxService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _drillBoxService = drillBoxService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(DrillBoxDto drillBoxDto)
        {
            try
            {
                var result = await _drillBoxService.Add(drillBoxDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add drillBox. Error: {ex.Message}");
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
        public async Task<IActionResult> Update(DrillBoxDto drillBoxDto)
        {
            try
            {
                var result = await _drillBoxService.Update(drillBoxDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update drillBox. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _drillBoxService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete drillBox. Error: {ex.Message}");
            }
        }
    
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.Get(pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByRegion")]
        public async Task<IActionResult> GetByRegion(int regionId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByRegion(regionId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDeposit")]
        public async Task<IActionResult> GetByDeposit(int depositId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByDeposit(depositId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMine")]
        public async Task<IActionResult> GetByMine(int mineId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByMine(mineId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByMineArea")]
        public async Task<IActionResult> GetByMineArea(int mineAreaId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByMineArea(mineAreaId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDrillHole")]
        public async Task<IActionResult> GetByDrillHole(int drillHoleId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _drillBoxService.GetByDrillHole(drillHoleId, pageParams);
                if(result == null) return NotFound("No drillBoxes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBoxes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _drillBoxService.GetById(id);
                if(result == null) return NotFound("No drillBox found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBox. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getDirSize")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDirSize()
        {
            try
            {
                var dir = "Resources/Images/DrillBoxes";
                double total = 0;
                if (Directory.Exists(dir)) 
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    FileInfo[] fiArr = di.GetFiles();
                    foreach(FileInfo f in fiArr) {
                        total += f.Length/1024;
                    }
                }
                return Ok((total/1024).ToString("0.##") + " Kb");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover drillBox. Error: {ex.Message}");
            }
        }

    }
}