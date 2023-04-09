using GB_Corporation.DTOs;
using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Services
{
    public interface IAuthService
    {
        bool IsExists(RegisterDTO model);
        bool IsExists(UpdatePasswordDTO model);
        void Register(RegisterDTO register);
        Employee GetUserByEmail(string email);
        EmployeeGetUserDTO GetUserById(int id, string? jwt);
        void UpdatePassword(UpdatePasswordDTO updatePassword);
    }
}
