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

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, ChiefAccountant, BA")]
        [HttpGet("GetAll")]
        public IActionResult GetAll([Required][FromHeader] string role, [Required][FromHeader] int userId, [FromQuery] string? nameEn = null, 
            [FromQuery] string? surnameEn = null, [FromQuery] string? login = null, [FromQuery] int?[] positionIds = null, 
            [FromQuery] int?[] statusIds = null)
        {
            var filters = new HiringFilterDTO(nameEn, surnameEn, login, positionIds, statusIds);

            return Ok(_hiringService.ListAll(userId, role, filters));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetInterviewers")]
        public IActionResult GetInterviewers()
        {
            return Ok(_hiringService.GetInterviewers());
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetTestData")]
        public IActionResult GetTestData([Required][FromHeader] int id)
        {
            return Ok(_hiringService.ListTestShort(id));
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetInterviewerPositions")]
        public IActionResult GetInterviewerPositions()
        {
            return Ok(_hiringService.GetInterviewerPositions());
        }

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, ChiefAccountant, BA")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (id < 1 || !_hiringService.IsExistsData(id))
                return BadRequest();

            var data = _hiringService.GetById(id);

            return Ok(data);
        }
        
        [Authorize(Roles = "Admin, HR")]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] HiringCreateDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_hiringService.IsExistsActiveData(model.ApplicantId))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.HiringExists));

            if (_hiringService.CheckCreateData(model))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.InvalidData));

            _hiringService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, ChiefAccountant, BA")]
        [HttpPut("UpdateDescription")]
        public IActionResult UpdateDescription([Required][FromHeader] int id, [Required][FromHeader] string description)
        {
            if (id < 1 || !_hiringService.IsExistsInterviewData(id))
                return BadRequest();

            _hiringService.UpdateDescription(id, description);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR, TeamLeader, LineManager, CEO, ChiefAccountant, BA")]
        [HttpGet("GetApplicantHiringData")]
        public IActionResult GetApplicantHiringData([Required][FromHeader] int id)
        {
            if (id < 1 || !_hiringService.IsExistsData(id))
                return BadRequest();

            var data = _hiringService.GetApplicantHiringData(id);

            return Ok(data);
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Reject")]
        public IActionResult Reject([Required][FromHeader] int id)
        {
            if (id < 1 || !_hiringService.IsExistsData(id))
                return BadRequest();

            _hiringService.Reject(id);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Hire")]
        public IActionResult Hire([FromBody] HiringAcceptDTO model)
        { 
            if (model.Id < 1 ||  !_hiringService.IsExistsData(model.Id))
                return BadRequest();

            _hiringService.Hire(model);

            return Ok();
        }
    }
}
