using GB_Corporation.DTOs;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
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
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public TestCompetenciesController(ITestCompetenciesService testCompetenciesService, ITemplateService templateService,
            IEmployeeService employeeService, EmailService emailService, IConfiguration configuration)
        {
            _testCompetenciesService = testCompetenciesService;
            _templateService = templateService;
            _employeeService = employeeService;
            _emailService = emailService;
            _configuration = configuration;
        }

        [Authorize(Roles = "Admin, LineManager")]
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

            EmailModel mailSettings = _configuration.GetSection("mailSettings").Get<EmailModel>();

            var emailNotification = new EmailDTO
            {
                Subject = "Сompetence testing",
                Text = String.Format(mailSettings.TestCompetencies),
                SendTo = new List<int> { model.EmployeeId },
            };
            
            _emailService.SendEmail(emailNotification);

            return Ok();
        }

        [Authorize(Roles = "Developer, LineManager, Admin, TeamLeader, Accountant, ChiefAccountant, BA")]
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

        [Authorize(Roles = "Developer, LineManager, Admin, TeamLeader, Accountant, ChiefAccountant, BA")]
        [HttpPost("Complete")]
        public IActionResult Complete([Required][FromBody] TestCompleteDTO model)
        {
            if(model == null)
                return BadRequest();

            _testCompetenciesService.Complete(model);

            return Ok();
        }

        [Authorize(Roles = "Developer, LineManager, Admin, TeamLeader, Accountant, ChiefAccountant, BA")]
        [HttpGet("GetUserTests")]
        public IActionResult GetUserTests([Required][FromHeader] int userId, [Required][FromHeader] string role, [FromQuery] string? login = null, 
            [FromQuery] string? test = null, [FromQuery] int?[] statusIds = null)
        {
            if(!_employeeService.IsExists(userId))
            {
                return BadRequest();
            }

            var filters = new TestProgressFilterDTO(login, test, statusIds);

            var testData = _testCompetenciesService.GetUserTests(userId, role, filters);

            return Ok(testData);
        }

        [Authorize(Roles = "Developer, LineManager, Admin, TeamLeader, Accountant, ChiefAccountant, BA")]
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
