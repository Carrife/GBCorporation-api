using GB_Corporation.DTOs.AuthenticationDTOs;
using GB_Corporation.DTOs.EmployeeDTOs;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IEmployeeRepository employeeRepository, IRoleRepository roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        public void Register(RegisterDTO register)
        {
            var employee = new Employee
            {
                NameEn = register.NameEn,
                SurnameEn = register.SurnameEn,
                PatronymicRu = register.PatronymicRu,
                NameRu = register.NameRu,
                SurnameRu = register.SurnameRu,
                Phone = register.Phone,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                LanguageId = register.LanguageId,
                DepartmentId = register.DepartmentId,
                RoleId = register.RoleId,
            };

            _employeeRepository.Create(employee);
        }

        public Employee GetUserByEmail(string email)
        {
            return _employeeRepository.GetByEmail(email);
        }

        public EmployeeGetUserDTO GetUserById(int id, string? jwt)
        {
            var emoployee =  _employeeRepository.GetById(id);

            var user = new EmployeeGetUserDTO
            {
                Name = emoployee.NameEn + " " + emoployee.SurnameEn,
                Role = _roleRepository.GetById(emoployee.RoleId).Title,
                Token = jwt
            };
           
            return user;
        }

        public void UpdatePassword(UpdatePasswordDTO updatePassword)
        {
            var user = _employeeRepository.GetAll().Where(x => x.NameEn + " " + x.SurnameEn == updatePassword.UserName).First();

            user.Password = BCrypt.Net.BCrypt.HashPassword(updatePassword.NewPassword);

            _employeeRepository.Update(user);
        }
    }
}
