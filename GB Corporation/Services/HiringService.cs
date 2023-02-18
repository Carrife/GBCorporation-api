using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;

namespace GB_Corporation.Services
{
    public class HiringService : IHiringService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantReporitory;
        private readonly IRepository<ApplicantHiringData> _applicantHiringDataRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<HiringData> _hiringDataRepository;
        private readonly IRepository<ApplicantForeignLanguageTest> _foreignLanguageTestRepository;
        private readonly IRepository<ApplicantLogicTest> _logicTestRepository;
        private readonly IRepository<ApplicantProgrammingTest> _programmingTestRepository;

        public HiringService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<ApplicantHiringData> applicantHiringDataRepository, IRepository<SuperDictionary> superDictionaryRepository,
             IRepository<HiringData> hiringDataRepository, IRepository<ApplicantForeignLanguageTest> foreignLanguageTestRepository,
             IRepository<ApplicantLogicTest> logicTestRepository, IRepository<ApplicantProgrammingTest> programmingTestRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _applicantHiringDataRepository = applicantHiringDataRepository;
            _superDictionaryRepository = superDictionaryRepository;
            _hiringDataRepository = hiringDataRepository;
            _foreignLanguageTestRepository = foreignLanguageTestRepository;
            _logicTestRepository = logicTestRepository;
            _programmingTestRepository = programmingTestRepository;
        }

        public List<ApplicantHiringDataDTO> ListAll()
        {
            return AutoMapperExpression.AutoMapApplicantHiringDataDTO(_applicantHiringDataRepository.GetListResultSpec(x => x)
                    .Include(x => x.Applicant)
                    .Include(x => x.TeamLeader)
                    .Include(x => x.LineManager)
                    .Include(x => x.ForeignLanguageTest).ThenInclude(x => x.ForeignLanguage)
                    .Include(x => x.ProgrammingTest).ThenInclude(x => x.ProgrammingLanguage)
                    .Include(x => x.LogicTest)
                    .Include(x => x.Status)
                .OrderBy(x => x.Applicant.NameEn).ThenBy(x => x.Applicant.SurnameEn));
        }

        public List<ShortDTO> ListForeignTestShort(int id)
        {
            return AutoMapperExpression.AutoMapShortDTO(_foreignLanguageTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                    .Include(x => x.ForeignLanguage)
                .OrderBy(x => x.ForeignLanguage)
                .OrderByDescending(x => x.Date));
        }

        public List<ShortDTO> ListLogicTestShort(int id)
        {
            return AutoMapperExpression.AutoMapShortDTO(_logicTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                .OrderByDescending(x => x.Date));
        }

        public List<ShortDTO> ListProgrammingTestShort(int id)
        {
            return AutoMapperExpression.AutoMapShortDTO(_programmingTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                    .Include(x => x.ProgrammingLanguage)
                .OrderBy(x => x.ProgrammingLanguage)
                .OrderByDescending(x => x.Date));
        }

        public bool IsExists(string login) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Login == login)) || _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == login));

        public bool IsExists(int id) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Id == id));

        public bool IsExistsActiveData(int applicantId)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus && 
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;
            return _applicantHiringDataRepository.GetResultSpec(x => x.Any(p => p.ApplicantId == applicantId && p.StatusId == statusId));
        } 

        public bool IsExistsData(int id) => _applicantHiringDataRepository.GetResultSpec(x => x.Any(p => p.Id == id));

        public List<ApplicantHiringDataDTO> GetById(int id)
        {
            return AutoMapperExpression.AutoMapApplicantHiringDataDTO(_applicantHiringDataRepository.GetListResultSpec(x => x.Where(p => p.Id == id))
                    .Include(x => x.Applicant)
                    .Include(x => x.TeamLeader)
                    .Include(x => x.LineManager)
                    .Include(x => x.ForeignLanguageTest).ThenInclude(x => x.ForeignLanguage)
                    .Include(x => x.ProgrammingTest).ThenInclude(x => x.ProgrammingLanguage)
                    .Include(x => x.LogicTest)
                    .Include(x => x.Status)
                .OrderBy(x => x.Applicant.NameEn).ThenBy(x => x.Applicant.SurnameEn));
        }

        public void CreateApplicantHiringData(ApplicantHiringDataDTO data)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus &&
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;

            var applicantHiringData = new ApplicantHiringData
            {
                TeamLeaderId = data.TeamLeader.Id,
                LineManagerId = data.LineManager.Id,
                ApplicantId = data.Applicant.Id,
                Date = data.Date,
                ForeignLanguageTestId = data.ForeignLanguageTest.Id,
                LogicTestId = data.LogicTest.Id,
                ProgrammingTestId = data.ProgrammingTest.Id,
                StatusId = statusId
            };

           _applicantHiringDataRepository.Create(applicantHiringData);
        }

        public void Update(ApplicantHiringDataDTO model)
        {
            var data = _applicantHiringDataRepository.GetById(model.Id);

            if (data != null)
            {
                //data.ForeignLanguageId = model.ForeignLanguage?.Id;
                //data.ForeignLanguageResult = model.ForeignLanguageResult;
                //data.ProgrammingLanguageId = model.ProgrammingLanguage?.Id;
                //data.ProgrammingLanguageResult = model.ProgrammingLanguageResult;
                data.TeamLeaderId = model.TeamLeader.Id;
                data.TeamLeaderDescription = model.TeamLeaderDescription;
                data.LineManagerId = model.LineManager.Id;
                data.LineManagerDescription = model.LineManagerDescription;

                _applicantHiringDataRepository.Update(data);
            }
        }

        public void Hire(HiringDTO model)
        {
            var applicant = _applicantReporitory.GetById(model.Id);
            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Hired))).First().Id;

            var employeeActiveStatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus && p.Name == nameof(EmployeeStatusEnum.Active))).First().Id;

            if(_employeeRepository.GetResultSpec(x => x.Any(p => p.Login == applicant.Login)))
            {
                var employee = _employeeRepository.GetResultSpec(x => x.Where(p => p.Login == applicant.Login)).First();
                employee.StatusId = employeeActiveStatusId;
                employee.Email = model.Email;
                employee.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                employee.LanguageId = model.LanguageId;
                employee.DepartmentId = model.DepartmentId;
                employee.RoleId = model.RoleId;

                _employeeRepository.Update(employee);
            }
            else
            {
                var employee = new Employee()
                {
                    NameRu = applicant.NameRu,
                    SurnameRu = applicant.SurnameRu,
                    PatronymicRu = applicant.PatronymicRu,
                    NameEn = applicant.NameEn,
                    SurnameEn = applicant.SurnameEn,
                    Login = applicant.Login,
                    Phone = applicant.Phone,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    LanguageId = model.LanguageId,
                    DepartmentId = model.DepartmentId,
                    RoleId = model.RoleId,
                    StatusId = employeeActiveStatusId
                };

                _employeeRepository.Create(employee);
            }

            _applicantReporitory.Update(applicant);
        }

        public void Reject(int id)
        {
            var applicant = _applicantReporitory.GetById(id);
            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Rejected))).First().Id;

            _applicantReporitory.Update(applicant);
        }
    }
}
