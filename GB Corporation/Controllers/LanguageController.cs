using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [Authorize(Roles = "Admin, Developer, LineManager, RootUser, TeamLeader")]
        [HttpGet("GetLanguages")]
        public IActionResult GetLanguages()
        {
            return Ok(_languageService.GetLanguages());
        }
    }
}
