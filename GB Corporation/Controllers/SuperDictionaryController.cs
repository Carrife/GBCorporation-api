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

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR")]
        [HttpGet("GetProgrammingLanguages")]
        public IActionResult GetProgrammingLanguages()
        {
            return Ok(_superDictionaryService.GetProgrammingLanguages());
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR")]
        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_superDictionaryService.GetDepartments());
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR")]
        [HttpGet("GetForeignLanguages")]
        public IActionResult GetForeignLanguages()
        {
            return Ok(_superDictionaryService.GetForeignLanguages());
        }
    }
}
