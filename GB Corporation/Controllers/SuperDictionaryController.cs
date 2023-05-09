using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperDictionaryController : ControllerBase
    {
        private readonly ISuperDictionaryService _superDictionaryService;

        public SuperDictionaryController(ISuperDictionaryService superDictionaryService)
        {
            _superDictionaryService = superDictionaryService;
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetProgrammingLanguages")]
        public IActionResult GetProgrammingLanguages()
        {
            return Ok(_superDictionaryService.GetProgrammingLanguages());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateProgrammingLanguage")]
        public IActionResult CreateProgrammingLanguage([FromBody] ShortDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_superDictionaryService.IsProgrammingLanguageExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.CreateProgrammingLanguage(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteProgrammingLanguage")]
        public IActionResult DeleteProgrammingLanguage([FromHeader] int id)
        {
            if (id < 1 || !_superDictionaryService.IsExists(id))
                return BadRequest();

            _superDictionaryService.DeleteProgrammingLanguage(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProgrammingLanguage")]
        public IActionResult UpdateProgrammingLanguage(ShortDTO model)
        {
            if (model == null || !_superDictionaryService.IsExists(model.Id))
                return BadRequest();

            if (_superDictionaryService.IsProgrammingLanguageExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.UpdateProgrammingLanguage(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, Accountant, ChiefAccountant, CEO, BA")]
        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_superDictionaryService.GetDepartments());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateDepartment")]
        public IActionResult CreateDepartment([FromBody] ShortDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_superDictionaryService.IsDepartmentExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.CreateDepartment(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteDepartment")]
        public IActionResult DeleteDepartment([FromHeader] int id)
        {
            if (id < 1 || !_superDictionaryService.IsExists(id))
                return BadRequest();

            _superDictionaryService.DeleteDepartment(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment(ShortDTO model)
        {
            if (model == null || !_superDictionaryService.IsExists(model.Id))
                return BadRequest();

            if (_superDictionaryService.IsDepartmentExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.UpdateDepartment(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetForeignLanguages")]
        public IActionResult GetForeignLanguages()
        {
            return Ok(_superDictionaryService.GetForeignLanguages());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateForeignLanguage")]
        public IActionResult CreateForeignLanguage([FromBody] ShortDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_superDictionaryService.IsForeignLanguageExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.CreateForeignLanguage(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteForeignLanguage")]
        public IActionResult DeleteForeignLanguage([FromHeader] int id)
        {
            if (id < 1 || !_superDictionaryService.IsExists(id))
                return BadRequest();

            _superDictionaryService.DeleteForeignLanguage(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateForeignLanguage")]
        public IActionResult UpdateForeignLanguage(ShortDTO model)
        {
            if (model == null || !_superDictionaryService.IsExists(model.Id))
                return BadRequest();

            if (_superDictionaryService.IsForeignLanguageExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.UpdateForeignLanguage(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, Accountant, ChiefAccountant, CEO, BA")]
        [HttpGet("GetPositions")]
        public IActionResult GetPositions()
        {
            return Ok(_superDictionaryService.GetPositions());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreatePosition")]
        public IActionResult CreatePosition([FromBody] ShortDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_superDictionaryService.IsPositionExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.CreatePosition(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeletePosition")]
        public IActionResult DeletePosition([FromHeader] int id)
        {
            if (id < 1 || !_superDictionaryService.IsExists(id))
                return BadRequest();

            _superDictionaryService.DeletePosition(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdatePosition")]
        public IActionResult UpdatePosition(ShortDTO model)
        {
            if (model == null || !_superDictionaryService.IsExists(model.Id))
                return BadRequest();

            if (_superDictionaryService.IsPositionExists(model.Name))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameDataExists));

            _superDictionaryService.UpdatePosition(model);

            return Ok();
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetEmployeeStatuses")]
        public IActionResult GetEmployeeStatuses()
        {
            return Ok(_superDictionaryService.GetEmployeeStatuses());
        }

        [Authorize(Roles = "Admin, HR")]
        [HttpGet("GetApplicantStatuses")]
        public IActionResult GetApplicantStatuses()
        {
            return Ok(_superDictionaryService.GetApplicantStatuses());
        }

        [Authorize(Roles = "Admin, LineManager, TeamLeader, HR, BA, CEO, ChiefAccountant")]
        [HttpGet("GetHiringStatuses")]
        public IActionResult GetHiringStatuses()
        {
            return Ok(_superDictionaryService.GetHiringStatuses());
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, BA, Accountant, CEO, ChiefAccountant")]
        [HttpGet("GetTestCompetenciesStatuses")]
        public IActionResult GetTestCompetenciesStatuses()
        {
            return Ok(_superDictionaryService.GetTestCompetenciesStatuses());
        }
    }
}
