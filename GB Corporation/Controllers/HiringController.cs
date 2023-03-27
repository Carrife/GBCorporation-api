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

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, Accountant, BA")]
        [HttpGet("GetAll")]
        public IActionResult GetAll([Required][FromHeader] string role, [Required][FromHeader] int userId)
        {
            return Ok(_hiringService.ListAll(userId, role));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetForeignTestShort")]
        public IActionResult GetForeignTestShort([Required][FromHeader] int id)
        {
            return Ok(_hiringService.ListForeignTestShort(id));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetLogicTestShort")]
        public IActionResult GetLogicTestShort([Required][FromHeader] int id)
        {
            return Ok(_hiringService.ListLogicTestShort(id));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetProgrammingTestShort")]
        public IActionResult GetProgrammingTestShort([Required][FromHeader] int id)
        {
            return Ok(_hiringService.ListProgrammingTestShort(id));
        }

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, Accountant, BA")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (!_hiringService.IsExistsData(id))
                return BadRequest();

            var data = _hiringService.GetById(id);

            return Ok(data);
        }
        
        [Authorize(Roles = "Admin, HR")]
        [HttpPost("Create")]
        public IActionResult CreateApplicantHiringData([FromBody] HiringDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_hiringService.IsExistsActiveData(model.Applicant.Id))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.HiringExists));

            _hiringService.CreateHiringData(model);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin, TeamLeader, LineManager")]
        [HttpPut("UpdateDescription")]
        public IActionResult UpdateDescription([Required][FromHeader] int id, [Required][FromHeader] string description)
        {
            if (!_hiringService.IsExistsInterviewData(id))
                return BadRequest();

            _hiringService.UpdateDescription(id, description);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Reject")]
        public IActionResult Reject([Required][FromHeader] int id)
        {
            if (!_hiringService.IsExistsData(id))
                return BadRequest();

            _hiringService.Reject(id);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Hire")]
        public IActionResult Hire([Required][FromHeader] HiringAcceptDTO model)
        { 
            if (!_hiringService.IsExistsData(model.Id))
                return BadRequest();

            _hiringService.Hire(model);

            return Ok();
        }
    }
}
