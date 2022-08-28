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

        [Authorize(Roles = "Admin, Developer, LineManager, RootUser, TeamLeader, HR")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_templateService.GetAll());
        }

        [Authorize(Roles = "Admin, RootUser, HR")]
        [HttpPost("Create")]
        public IActionResult Create(TemplateCreateDTO model)
        {
            if(model == null || _templateService.IsExists(model.Name))
                return BadRequest();

            _templateService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, HR")]
        [HttpPut("Update")]
        public IActionResult Update(TemplateDTO model)
        {
            if (model == null || !_templateService.IsExists(model.Id) || _templateService.IsExists(model.Name))
                return BadRequest();

            _templateService.Update(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, HR")]
        [HttpPost("Delete")]
        public IActionResult Delete([FromHeader]int id)
        {
            if (!_templateService.IsExists(id))
                return BadRequest();

            _templateService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, HR")]
        [HttpPost("Upload")]
        public IActionResult UploadFile([Required][FromHeader] int id, IFormFile file)
        {
            if (file == null || id < 1)
                return BadRequest();

            if(!_templateService.IsExists(id))
                return NotFound();

            _templateService.Upload(file, id);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, HR")]
        [HttpGet("Download")]
        public IActionResult DownloadFile([Required][FromHeader] int id)
        {
            if (!_templateService.IsDocExists(id))
                return NotFound();

            string path = _templateService.GetFilePath(id);
            FileStream fs = new FileStream(path, FileMode.Open);
            string fileType = "application/pdf";
            string file_name = _templateService.GetFileName(id);
            
            return File(fs, fileType, file_name);
        }
    }
}
