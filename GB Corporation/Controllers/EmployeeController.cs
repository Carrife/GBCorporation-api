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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, BA, Accountant, CEO, ChiefAccountant")]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] string? nameRu = null, [FromQuery] string? surnameRu = null, 
            [FromQuery] string? patronymicRu = null, [FromQuery] string? nameEn = null, [FromQuery] string? surnameEn = null, 
            [FromQuery] string? login = null, [FromQuery] int?[] departmentIds = null, [FromQuery] int?[] positionIds = null, 
            [FromQuery] int?[] statusIds = null)
        {
            var filters = new EmployeeFilterDTO(nameRu, surnameRu, patronymicRu, nameEn, surnameEn, login, departmentIds, positionIds, statusIds);

            return Ok(_employeeService.ListAll(filters));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers([FromQuery] string? nameRu = null, [FromQuery] string? surnameRu = null,
            [FromQuery] string? patronymicRu = null, [FromQuery] string? nameEn = null, [FromQuery] string? surnameEn = null,
            [FromQuery] string? login = null, [FromQuery] int?[] roleIds = null)
        {
            var filters = new UsersFilterDTO(nameRu, surnameRu, patronymicRu, nameEn, surnameEn, login, roleIds);

            return Ok(_employeeService.ListAllUsers(filters));
        }

        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, BA, Accountant, CEO, ChiefAccountant")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (id < 1 || !_employeeService.IsExists(id))
                return BadRequest();
            
            var employee = _employeeService.GetById(id);

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public IActionResult Create(EmployeeCreateDTO model)
        {
            if (model == null)
                return BadRequest();

            if (_employeeService.IsExists(model.Login))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.SameLoginExists));

            _employeeService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Fired")]
        public IActionResult Fired([Required][FromHeader]int id)
        {
            if (id < 1 || !_employeeService.IsExists(id))
                return BadRequest();

            _employeeService.Fired(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public IActionResult Update(EmployeeUpdateDTO model)
        {
            if (model == null || !_employeeService.IsExists(model.Id))
                return BadRequest();

            _employeeService.Update(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserUpdateDTO model)
        {
            if (model == null || !_employeeService.IsExists(model.Id))
                return BadRequest();

            _employeeService.UpdateUser(model);

            return Ok();
        }
    }
}
