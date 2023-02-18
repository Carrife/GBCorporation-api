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

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_hiringService.ListAll());
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

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (!_hiringService.IsExistsData(id))
                return NotFound();

            var data = _hiringService.GetById(id);

            return Ok(data);
        }
        
        [Authorize(Roles = "Admin, HR")]
        [HttpPost("Create")]
        public IActionResult CreateApplicantHiringData([FromBody] ApplicantHiringDataDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_hiringService.IsExistsActiveData(model.Applicant.Id))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.HiringExists));

            _hiringService.CreateApplicantHiringData(model);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
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

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Reject")]
        public IActionResult Reject(int id)
        {
            if (!_hiringService.IsExists(id))
                return NotFound();

            _hiringService.Reject(id);

            return Ok();
        }

        [Authorize(Roles = "HR, Admin")]
        [HttpPut("Hire")]
        public IActionResult Hire(HiringDTO model)
        { 
            if (!_hiringService.IsExists(model.Id))
                return NotFound();

            _hiringService.Hire(model);

            return Ok();
        }
    }
}
