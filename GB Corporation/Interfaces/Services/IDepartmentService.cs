using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IDepartmentService
    {
        List<ShortDTO> GetDepartments();
    }
}
