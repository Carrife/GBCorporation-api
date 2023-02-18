using GB_Corporation.DTOs;
using GB_Corporation.DTOs.EmployeeDTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IEmployeeService
    {
        bool IsExists (int id);
        List<EmployeeDTO> ListAll();
        List<ShortDTO> ListLMShort();
        List<ShortDTO> ListTLShort();
        void Delete(int id);
        void Update(EmployeeUpdateDTO model);
        EmployeeWithTestsDTO GetById(int id);
    }
}
