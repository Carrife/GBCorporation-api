using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Helpers;
using GB_Corporation.DTOs.EmployeeDTOs;
using Microsoft.EntityFrameworkCore;
using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<TestCompetencies> _testCompetenciesReporitory;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<TestCompetencies> testCompetenciesReporitory,
             IRepository<Role> roleRepository, IRepository<SuperDictionary> superDictionaryRepository)
        {
            _employeeRepository = employeeRepository;
            _testCompetenciesReporitory = testCompetenciesReporitory;
            _roleRepository = roleRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }

        public List<EmployeeDTO> ListAll()
        {
            return AutoMapperExpression.AutoMapEmployee(_employeeRepository.GetListResultSpec(x => x)
                    .Include(x => x.Role)
                    .Include(x => x.Department)
                    .Include(x => x.Language)
                    .OrderBy(x => x.NameEn).ToList());
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(_employeeRepository.GetResultSpec(x => x.Where(p => p.Id == id)).FirstOrDefault());
        }

        public void Update(EmployeeUpdateDTO model)
        {
            var user = _employeeRepository.GetById(model.Id);

            if (user != null)
            {
                user.NameRu = model.NameRu;
                user.SurnameRu = model.SurnameRu;
                user.PatronymicRu = model.PatronymicRu;
                user.NameEn = model.NameEn;
                user.SurnameEn = model.SurnameEn;
                user.WorkPhone = model.WorkPhone;
                user.Phone = model.Phone;
                user.LanguageId = model.Language?.Id;
                user.DepartmentId = model.Department.Id;

                _employeeRepository.Update(user);
            }
        }

        public EmployeeWithTestsDTO GetById(int id)
        {
            var user = AutoMapperExpression.AutoMapEmployee(_employeeRepository.GetListResultSpec(x => x)
                .Include(x => x.Role)
                .Include(x => x.Department)
                .Include(x => x.Language)
                .Where(x => x.Id == id)
                .First());
            
            var tests = AutoMapperExpression.AutoMapTestCompetenciesDTO(_testCompetenciesReporitory.GetListResultSpec(x => x.Where(p => p.EmployeeId == id)).ToList());

            var employee = new EmployeeWithTestsDTO
            {
                Employee = user,
                TestCompetencies = tests
            };

            return employee;
        }

        public List<ShortDTO> GetLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Language)).ToList());
        }

        public List<ShortDTO> GetDepartments()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Department)).ToList());
        }

        public List<ShortDTO> GetRoles()
        {
            return AutoMapperExpression.AutoMapShortDTO(_roleRepository.GetListResultSpec(x => x).ToList());
        }
    }
}
