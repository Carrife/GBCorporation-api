using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IEmployeeService
    {
        bool IsExists (int id);
        bool IsExists(string login);
        List<EmployeeDTO> ListAll(EmployeeFilterDTO filters);
        void Create(EmployeeCreateDTO model);
        void Fired(int id);
        void Update(EmployeeUpdateDTO model);
        EmployeeGetDTO GetById(int id);
        List<UserDTO> ListAllUsers(UsersFilterDTO filters);
        void UpdateUser(UserUpdateDTO model);
        string GetCV(int id);
        List<ShortDTO> ListAllShort();
    }
}
