using GB_Corporation.DTOs.AuthenticationDTOs;
using GB_Corporation.DTOs.EmployeeDTOs;
using GB_Corporation.Enums;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public AuthService(IRepository<Employee> employeeRepository, IRepository<Role> roleRepository, IRepository<SuperDictionary> superDictionaryRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }
        
        public bool IsExists(RegisterDTO model) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == model.Login || p.Email == model.Email));

        public bool IsExists(UpdatePasswordDTO model) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == model.Login));

        public void Register(RegisterDTO register)
        {
            var employee = new Employee
            {
                NameEn = register.NameEn,
                SurnameEn = register.SurnameEn,
                PatronymicRu = register.PatronymicRu,
                NameRu = register.NameRu,
                SurnameRu = register.SurnameRu,
                Login = register.Login,
                Phone = register.Phone,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                LanguageId = register.LanguageId,
                DepartmentId = register.DepartmentId,
                RoleId = register.RoleId,
                PositionId = register.PositionId,
                StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus && p.Name == nameof(EmployeeStatusEnum.Active))).First().Id,
            };

            _employeeRepository.Create(employee);
        }

        public Employee GetUserByEmail(string email) => _employeeRepository.GetResultSpec(x => x.Where(p => p.Email == email)).FirstOrDefault();

        public EmployeeGetUserDTO GetUserById(int id, string? jwt)
        {
            var emoployee =  _employeeRepository.GetById(id);

            var user = new EmployeeGetUserDTO
            {
                Id = id,
                Name = emoployee.NameEn + " " + emoployee.SurnameEn,
                Role = _roleRepository.GetById(emoployee.RoleId.Value).Title,
                Token = jwt
            };
           
            return user;
        }

        public void UpdatePassword(UpdatePasswordDTO updatePassword)
        {
            var user = _employeeRepository.GetListResultSpec(x => x.Where(p => p.Login == updatePassword.Login)).First();

            user.Password = BCrypt.Net.BCrypt.HashPassword(updatePassword.NewPassword);

            _employeeRepository.Update(user);
        }
    }
}
