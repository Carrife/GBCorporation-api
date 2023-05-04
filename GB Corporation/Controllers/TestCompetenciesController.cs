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

        [Authorize(Roles = "Admin, LineManager")]
        [HttpPost("Create")]
        public IActionResult Create(TestCreateDTO model)
        {
            if (model == null || model.EmployeeId < 1 || !_employeeService.IsExists(model.EmployeeId))
                return BadRequest();

            _testCompetenciesService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "TeamLeader, Developer, Admin")]
        [HttpGet("Start")]
        public IActionResult Start([Required][FromHeader] int id)
        {
            if (id < 1 || !_testCompetenciesService.IsDocExists(id))
                return BadRequest();

            int docId = _testCompetenciesService.GetTemplateId(id);

            string docPath = _templateService.GetFilePath(docId);

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

        [Authorize(Roles = "HR, TeamLeader, Developer, Admin")]
        [HttpGet("GetUserTests")]
        public IActionResult GetUserTests([FromHeader] int? id, [FromQuery] string? login = null, [FromQuery] string? test = null, 
            [FromQuery] int?[] statusIds = null)
        {
            if (id != null)
            {
                if(!_employeeService.IsExists((int)id))
                {
                    return BadRequest();
                }
            }

            var filters = new TestProgressFilterDTO(login, test, statusIds);

            var testData = _testCompetenciesService.GetUserTests(id, filters);

            return Ok(testData);
        }

        [Authorize(Roles = "HR, TeamLeader, Developer, Admin")]
        [HttpGet("GetUserResults")]
        public IActionResult GetUserResults([FromHeader] int? id, [FromQuery] string? login = null, [FromQuery] string? test = null)
        {
            if (id != null)
            {
                if (!_employeeService.IsExists((int)id))
                {
                    return BadRequest();
                }
            }

            var filters = new TestResultFilterDTO(login, test);

            var testData = _testCompetenciesService.GetUserResults(id, filters);

            return Ok(testData);
        }
    }
}
