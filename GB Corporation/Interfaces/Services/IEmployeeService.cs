using GB_Corporation.DTOs;
using GB_Corporation.DTOs.EmployeeDTOs;
using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> ListAll();
        void Delete(int id);
        void Update(EmployeeUpdateDTO model);
        EmployeeWithTestsDTO GetById(int id);
        List<ShortDTO> GetLanguages();
        List<ShortDTO> GetDepartments();
        List<ShortDTO> GetRoles();
    }
}
