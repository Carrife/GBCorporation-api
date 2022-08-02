using GB_Corporation.DTOs.AuthenticationDTOs;
using GB_Corporation.DTOs.HiringsDTOs;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("CreateApplicant")]
        public IActionResult CreateApplicant([FromBody] ApplicantDTO model)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest();

            _hiringService.CreateApplicant(model);

            return Ok("success");
        }

        [Authorize(Roles = "RootUser, HR")]
        [HttpPost("CreateApplicantHiringData")]
        public IActionResult CreateApplicantHiringData([FromBody] ApplicantHiringDataDTO model)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest();

            _hiringService.CreateApplicantHiringData(model);

            return Ok("success");
        }
    }
}
