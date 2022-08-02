using GB_Corporation.DTOs.AuthenticationDTOs;
using GB_Corporation.DTOs.EmployeeDTOs;
using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Services
{
    public interface IAuthService
    {
        public bool IsExists(RegisterDTO model);
        void Register(RegisterDTO register);
        Employee GetUserByEmail(string email);
        EmployeeGetUserDTO GetUserById(int id, string? jwt);
        void UpdatePassword(UpdatePasswordDTO updatePassword);
    }
}
