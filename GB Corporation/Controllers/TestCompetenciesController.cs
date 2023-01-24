using GB_Corporation.DTOs.TestCompetenciesDTOs;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCompetenciesController : ControllerBase
    {
        private readonly ITestCompetenciesService _testCompetenciesService;
        private readonly ITemplateService _templateService;
        public TestCompetenciesController(ITestCompetenciesService testCompetenciesService, ITemplateService templateService)
        {
            _testCompetenciesService = testCompetenciesService;
            _templateService = templateService;
        }

        [Authorize(Roles = "TeamLeader, Developer, Admin")]
        [HttpGet("Start")]
        public IActionResult Start([Required][FromHeader] int id)
        {
            if (!_templateService.IsExists(id))
                return NotFound();

            string docPath = _templateService.GetFilePath(id);

            var testData = _testCompetenciesService.GetTestData(docPath);

            return Ok(testData);
        }

        [Authorize(Roles = "TeamLeader, Developer, Admin")]
        [HttpPost("Complete")]
        public IActionResult Complete([Required][FromBody] TestCompleteDTO model)
        {
            if(model == null)
                return BadRequest();

            _testCompetenciesService.Complete(model);

            return Ok();
        }
    }
}
