using GB_Corporation.DTOs;
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
        private readonly IEmployeeService _employeeService;
        public TestCompetenciesController(ITestCompetenciesService testCompetenciesService, ITemplateService templateService,
            IEmployeeService employeeService)
        {
            _testCompetenciesService = testCompetenciesService;
            _templateService = templateService;
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Admin, Developer, TeamLeader")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_testCompetenciesService.GetAll());
        }

        [Authorize(Roles = "TeamLeader, Developer, Admin")]
        [HttpGet("Start")]
        public IActionResult Start([Required][FromHeader] int id)
        {
            if (id < 1 || !_templateService.IsDocExists(id))
                return BadRequest();

            string docPath = _templateService.GetFilePath(id);

            var testData = _testCompetenciesService.GetTestData(docPath);

            return Ok(testData);
        }

        [Authorize(Roles = "TeamLeader, Developer, Admin")]
        [HttpPost("Complete")]
        public IActionResult Complete([Required][FromBody] TestCompleteDTO model)
        {
            if(model == null || !_employeeService.IsExists(model.UserId))
                return BadRequest();

            _testCompetenciesService.Complete(model);

            return Ok();
        }

        [Authorize(Roles = "HR, TeamLeader, Developer, Admin")]
        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId([Required][FromHeader] int id)
        {
            if (id < 1 ||!_employeeService.IsExists(id))
                return BadRequest();

            var testData = _testCompetenciesService.GetUserTests(id);

            return Ok(testData);
        }
    }
}
