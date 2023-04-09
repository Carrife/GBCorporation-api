using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Helpers;
using GB_Corporation.DTOs;
using Microsoft.EntityFrameworkCore;
using GB_Corporation.Models;
using GB_Corporation.Enums;

namespace GB_Corporation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantRepository, 
            IRepository<SuperDictionary> superDictionaryRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantRepository = applicantRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }

        public bool IsExists(int id) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        public bool IsExists(string login) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == login)) || 
            _applicantRepository.GetResultSpec(x => x.Any(p => p.Login == login));

        public List<EmployeeDTO> ListAll()
        {
            return AutoMapperExpression.AutoMapEmployeeDTO(_employeeRepository.GetListResultSpec(x => x)
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.Status)
                .OrderBy(x => x.NameEn));
        }

        public void Create(EmployeeCreateDTO model)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus &&
                p.Name == nameof(EmployeeStatusEnum.Active))).First().Id;

            var user = new Employee
            {
                NameEn = model.NameEn,
                SurnameEn = model.SurnameEn,
                NameRu = model.NameRu,
                SurnameRu = model.SurnameRu,
                PatronymicRu = model.PatronymicRu,
                Login = model.Login,
                Phone = model.Phone,
                LanguageId = model.LanguageId,
                DepartmentId = model.DepartmentId,
                PositionId = model.PositionId,
                StatusId = statusId
            };
            
            _employeeRepository.Create(user);
        }

        public void Fired(int id)
        {
            var employee = _employeeRepository.GetById(id);

            employee.StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus &&
                p.Name == nameof(EmployeeStatusEnum.Fired))).First().Id;

            employee.RoleId = null;
            employee.WorkPhone = null;
            employee.Password = null;
            employee.Email = null;

            if(_applicantRepository.GetResultSpec(x => x.Any(p => p.Login == employee.Login)))
            {
                var applicant = _applicantRepository.GetResultSpec(x => x.Where(p => p.Login == employee.Login)).First();
                applicant.StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus &&
                    p.Name == nameof(ApplicantStatusEnum.Active))).First().Id;

                _applicantRepository.Update(applicant);
            }

            _employeeRepository.Update(employee);
        }

        public void Update(EmployeeUpdateDTO model)
        {
            var user = _employeeRepository.GetById(model.Id);

            user.NameRu = model.NameRu;
            user.SurnameRu = model.SurnameRu;
            user.PatronymicRu = model.PatronymicRu;
            user.NameEn = model.NameEn;
            user.SurnameEn = model.SurnameEn;
            user.WorkPhone = model.WorkPhone;
            user.Phone = model.Phone;
            user.LanguageId = model.LanguageId;
            user.DepartmentId = model.DepartmentId;
            user.PositionId = model.PositionId;

            _employeeRepository.Update(user);
        }

        public EmployeeGetDTO GetById(int id)
        {
           return AutoMapperExpression.AutoMapEmployeeGetDTO(_employeeRepository.GetById(id));
        }
    }
}
