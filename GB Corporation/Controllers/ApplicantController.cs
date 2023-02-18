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
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_applicantService.ListAll());
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetActiveShort")]
        public IActionResult GetActiveShort()
        {
            return Ok(_applicantService.ListActiveShort());
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] ApplicantCreateDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_applicantService.IsExists(model.Login))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameLoginExists));

            _applicantService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Update")]
        public IActionResult Update(ApplicantUpdateDTO model)
        {
            if (model == null || !_applicantService.IsExists(model.Id))
                return BadRequest();

            _applicantService.Update(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetTestDatas")]
        public IActionResult GetTestDatas([Required][FromHeader] int id)
        {
            return Ok(_applicantService.GetTestDatas(id));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpPost("CreateLogicTestData")]
        public IActionResult CreateLogicTestData([FromBody] LogicTestDTO model)
        {
            if (model == null)
                return BadRequest();

            _applicantService.CreateTestData(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpPost("CreateProgrammingTestData")]
        public IActionResult CreateProgrammingTestData([FromBody] ProgrammingTestDTO model)
        {
            if (model == null)
                return BadRequest();

            _applicantService.CreateTestData(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpPost("CreateForeignLanguageTestData")]
        public IActionResult CreateForeignLanguageTestData([FromBody] ForeignLanguageTestDTO model)
        {
            if (model == null)
                return BadRequest();

            _applicantService.CreateTestData(model);

            return Ok();
        }
    }
}
