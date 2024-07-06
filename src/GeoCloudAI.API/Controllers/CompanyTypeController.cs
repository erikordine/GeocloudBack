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
    public class CompanyTypeController : ControllerBase
    {  
        private readonly ICompanyTypeService _companyTypeService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public CompanyTypeController(ICompanyTypeService companyTypeService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _companyTypeService = companyTypeService;
        }
    
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(CompanyTypeDto companyTypeDto)
        {
            try
            {
                var result = await _companyTypeService.Add(companyTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add companyType. Error: {ex.Message}");
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
        public async Task<IActionResult> Update(CompanyTypeDto companyTypeDto)
        {
            try
            {
                var result = await _companyTypeService.Update(companyTypeDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update companyType. Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _companyTypeService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete companyType. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _companyTypeService.Get(pageParams);
                if(result == null) return NotFound("No companyTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover companyTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _companyTypeService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No companyTypes found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover companyTypes. Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _companyTypeService.GetById(id);
                if(result == null) return NotFound("No companyType found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover companyType. Error: {ex.Message}");
            }
        }

    }
}