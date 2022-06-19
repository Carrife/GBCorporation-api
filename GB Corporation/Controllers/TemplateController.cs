using GB_Corporation.DTOs.TemplateDTOs;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [Authorize(Roles = "Admin, Developer, LineManager, RootUser, TeamLeader")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_templateService.GetAll());
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpPost("Create")]
        public IActionResult Create(TemplateCreateDTO model)
        {
            if(!ModelState.IsValid || model == null)
                return BadRequest();

            _templateService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpPut("Update")]
        public IActionResult Update(TemplateDTO model)
        {
            if (!ModelState.IsValid || model == null || model.Id < 1)
                return BadRequest();

            _templateService.Update(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpPost("Delete")]
        public IActionResult Delete([FromHeader]int id)
        {
            if (id < 1)
                return BadRequest();

            _templateService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpPost("Upload")]
        public IActionResult UploadFile([Required][FromHeader] int id, IFormFile file)
        {
            if (file == null || id < 1)
                return BadRequest();

            bool isExists = _templateService.IsExists(id);

            if(!isExists)
                return NotFound();

            _templateService.Upload(file, id);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpGet("Download")]
        public IActionResult DownloadFile([Required][FromHeader] int id)
        {
            bool isExists = _templateService.IsDocExists(id);
            if (!isExists)
                return NotFound();

            string path = _templateService.GetFilePath(id);
            FileStream fs = new FileStream(path, FileMode.Open);
            string fileType = "application/pdf";
            string file_name = _templateService.GetFileName(id);
            
            return File(fs, fileType, file_name);
        }
    }
}
