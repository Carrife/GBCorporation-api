using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringController : ControllerBase
    {
        private readonly IHiringService _hiringService;
        public HiringController(IHiringService hiringService)
        {
            _hiringService = hiringService;
        }

        [Authorize(Roles = "RootUser, HR")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_hiringService.ListAll());
        }

        [Authorize(Roles = "RootUser, HR")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (!_hiringService.IsExists(id))
                return NotFound();

            var applicant = _hiringService.GetById(id);

            return Ok(applicant);
        }

        [Authorize(Roles = "RootUser, HR")]
        [HttpPost("CreateApplicant")]
        public IActionResult CreateApplicant([FromBody] ApplicantDTO model)
        {
            if (model == null)
                return BadRequest();

            if(_hiringService.IsExists(model.Login))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameLoginExists));

            _hiringService.CreateApplicant(model);

            return Ok();
        }

        [Authorize(Roles = "RootUser, HR")]
        [HttpPost("CreateApplicantHiringData")]
        public IActionResult CreateApplicantHiringData([FromBody] ApplicantHiringDataDTO model)
        {
            if (model == null || !_hiringService.IsExists(model.ApplicantId))
                return BadRequest();

            _hiringService.CreateApplicantHiringData(model);

            return Ok();
        }

        [Authorize(Roles = "HR, RootUser")]
        [HttpPut("Update")]
        public IActionResult Update(ApplicantHiringDataDTO model)
        {
            if (model == null)
                return BadRequest();

            if (!_hiringService.IsExistsData(model.Id))
                return NotFound();

            _hiringService.Update(model);

            return Ok();
        }
    }
}
