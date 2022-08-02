﻿using GB_Corporation.DTOs.EmployeeDTOs;
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

        [Authorize(Roles = "Admin, Developer, LineManager, RootUser, TeamLeader")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.ListAll());
        }

        [Authorize(Roles = "Admin, Developer, LineManager, RootUser, TeamLeader")]
        [HttpGet("GetById")]
        public IActionResult GetById([Required][FromHeader] int id)
        {
            if (id < 1)
                return BadRequest();
            
            if(_employeeService.IsExists(id))
                return NotFound();
            
            var employee = _employeeService.GetById(id);

            return Ok(employee);
        }

        [Authorize(Roles = "Admin, RootUser")]
        [HttpPost("Delete")]
        public IActionResult Delete([FromHeader]int id)
        {
            if (id < 1)
                return BadRequest();

            if (_employeeService.IsExists(id))
                return NotFound();

            _employeeService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = "Admin, RootUser, LineManager")]
        [HttpPut("Update")]
        public IActionResult Update(EmployeeUpdateDTO model)
        {
            if (model == null || model.Id < 1)
                return BadRequest();

            if (_employeeService.IsExists(model.Id))
                return NotFound();

            _employeeService.Update(model);

            return Ok();
        }
    }
}
