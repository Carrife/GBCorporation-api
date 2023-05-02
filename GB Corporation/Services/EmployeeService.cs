using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Helpers;
using GB_Corporation.DTOs;
using Microsoft.EntityFrameworkCore;
using GB_Corporation.Models;
using GB_Corporation.Enums;
using System.Linq.Expressions;
using Word = Microsoft.Office.Interop.Word;

namespace GB_Corporation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantRepository, 
            IRepository<SuperDictionary> superDictionaryRepository, IWebHostEnvironment appEnvironment)
        {
            _employeeRepository = employeeRepository;
            _applicantRepository = applicantRepository;
            _superDictionaryRepository = superDictionaryRepository;
            _appEnvironment = appEnvironment;
        }

        public bool IsExists(int id) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        public bool IsExists(string login) => _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == login)) || 
            _applicantRepository.GetResultSpec(x => x.Any(p => p.Login == login));

        public List<EmployeeDTO> ListAll(EmployeeFilterDTO filters)
        {
            var predicate = CreatePredicateEmployee(filters, out bool IsFilterActive);
            
            var totalElements = _employeeRepository.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<EmployeeDTO>();

            return AutoMapperExpression.AutoMapEmployeeDTO(_employeeRepository.GetListResultSpec(x => x.Where(predicate))
                .Include(x => x.Position)
                .Include(x => x.Department)
                .Include(x => x.Status)
                .OrderBy(x => x.NameEn));
        }

        private Expression<Func<Employee, bool>> CreatePredicateEmployee(EmployeeFilterDTO filters, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<Employee>();
            IsFilterActive = false;

            if (!string.IsNullOrEmpty(filters.NameRu))
            {
                predicate = predicate.And(p => p.NameRu.ToLower().Contains(filters.NameRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.SurnameRu))
            {
                predicate = predicate.And(p => p.SurnameRu.ToLower().Contains(filters.SurnameRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.PatronymicRu))
            {
                predicate = predicate.And(p => p.PatronymicRu.ToLower().Contains(filters.PatronymicRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.NameEn))
            {
                predicate = predicate.And(p => p.NameEn.ToLower().Contains(filters.NameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.SurnameEn))
            {
                predicate = predicate.And(p => p.SurnameEn.ToLower().Contains(filters.SurnameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.Login))
            {
                predicate = predicate.And(p => p.Login.ToLower() == filters.Login.ToLower());
                IsFilterActive = true;
            }

            if (filters.DepartmentIds != null && filters.DepartmentIds.Length > 0)
            {
                predicate = predicate.And(p => filters.DepartmentIds.Contains(p.DepartmentId));
                IsFilterActive = true;
            }

            if (filters.PositionIds != null && filters.PositionIds.Length > 0)
            {
                predicate = predicate.And(p => filters.PositionIds.Contains(p.PositionId));
                IsFilterActive = true;
            }

            if (filters.StatusIds != null && filters.StatusIds.Length > 0)
            {
                predicate = predicate.And(p => filters.StatusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }
            else
            {
                var statusIds = _superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.Name == nameof(EmployeeStatusEnum.Active)
                    && p.DictionaryId == (int)DictionaryEnum.EmployeeStatus)).Select(x => x.Id).ToList();

                predicate = predicate.And(p => statusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }

            return predicate;
        }

        public List<UserDTO> ListAllUsers(UsersFilterDTO filters)
        {
            var predicate = CreatePredicateUser(filters, out bool IsFilterActive);

            predicate = predicate.And(p => p.Status.Name == nameof(EmployeeStatusEnum.Active) && p.Status.DictionaryId == (int)DictionaryEnum.EmployeeStatus);

            var totalElements = _employeeRepository.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<UserDTO>();

            return AutoMapperExpression.AutoMapUserDTO(_employeeRepository.GetListResultSpec(x => x
                .Where(predicate))
                .Include(x => x.Role)
                .OrderBy(x => x.NameEn));
        }

        private Expression<Func<Employee, bool>> CreatePredicateUser(UsersFilterDTO filters, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<Employee>();
            IsFilterActive = false;

            if (!string.IsNullOrEmpty(filters.NameRu))
            {
                predicate = predicate.And(p => p.NameRu.ToLower().Contains(filters.NameRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.SurnameRu))
            {
                predicate = predicate.And(p => p.SurnameRu.ToLower().Contains(filters.SurnameRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.PatronymicRu))
            {
                predicate = predicate.And(p => p.PatronymicRu.ToLower().Contains(filters.PatronymicRu.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.NameEn))
            {
                predicate = predicate.And(p => p.NameEn.ToLower().Contains(filters.NameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.SurnameEn))
            {
                predicate = predicate.And(p => p.SurnameEn.ToLower().Contains(filters.SurnameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.Login))
            {
                predicate = predicate.And(p => p.Login.ToLower().Contains(filters.Login.ToLower()));
                IsFilterActive = true;
            }

            if (filters.RoleIds != null && filters.RoleIds.Length > 0)
            {
                predicate = predicate.And(p => filters.RoleIds.Contains(p.RoleId));
                IsFilterActive = true;
            }

            return predicate;
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

            var applicantStatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus &&
                    p.Name == nameof(ApplicantStatusEnum.Active))).First().Id;

            if(_applicantRepository.GetResultSpec(x => x.Any(p => p.Login == employee.Login)))
            {
                var applicant = _applicantRepository.GetResultSpec(x => x.Where(p => p.Login == employee.Login)).First();
                applicant.StatusId = applicantStatusId;

                _applicantRepository.Update(applicant);
            }
            else
            {
                var applicant = new Applicant
                {
                    NameEn = employee.NameEn,
                    SurnameEn = employee.SurnameEn,
                    NameRu = employee.NameRu,
                    SurnameRu = employee.SurnameRu,
                    PatronymicRu = employee.PatronymicRu,
                    Login = employee.Login,
                    Phone = employee.Phone,
                    StatusId = applicantStatusId
                };

                _applicantRepository.Create(applicant);
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
           return AutoMapperExpression.AutoMapEmployeeGetDTO(_employeeRepository.GetResultSpec(x => x.Where(p => p.Id == id)
           .Include(x => x.Status)
           .Include(x => x.Position)
           .Include(x => x.Department)
           .Include(x => x.Language))
           .First());
        }

        public void UpdateUser(UserUpdateDTO model)
        {
            var user = _employeeRepository.GetById(model.Id);

            user.RoleId = model.Role;
            user.Email = model.Email;
            if(model.Password != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }
            
            _employeeRepository.Update(user);
        }

        public string GetCV(int id)
        {
            var employee = _employeeRepository.GetResultSpec(x => x.Where(p => p.Id == id)).Include(x => x.Position).First();
            var pathTemplate = Path.Combine(_appEnvironment.ContentRootPath, @"Files\CV\CV.docx");
            var pathResult = Path.Combine(_appEnvironment.ContentRootPath, $@"Files\CV\CV {employee.NameEn} {employee.SurnameEn}.docx");

            var wordApplication = new Word.Application();
            

            try
            {
                wordApplication.Visible = false;
                
                var wordDoc = wordApplication.Documents.Open(pathTemplate);
                FormatingCV("<name>", $"{employee.NameEn} {employee.SurnameEn}", wordDoc);
                FormatingCV("<position>", $"{employee.Position.Name}", wordDoc);

                wordDoc.SaveAs2(pathResult);

                wordDoc.Close();
            }
            catch
            {
                Console.WriteLine("Doc error");
            }

            return pathResult;
        }

        private void FormatingCV(string strToReplace, string data, Word.Document doc)
        {
            var content = doc.Content;
            content.Find.ClearFormatting();
            content.Find.Execute(FindText: strToReplace, ReplaceWith: data);
        }
    }
}
