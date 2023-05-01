using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GB_Corporation.Services
{
    public class HiringService : IHiringService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantReporitory;
        private readonly IRepository<HiringInterviewer> _hiringInterviewerRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<HiringData> _hiringDataRepository;
        private readonly IRepository<HiringTestData> _hiringTestDataRepository;
        private readonly IRepository<ApplicantForeignLanguageTest> _foreignLanguageTestRepository;
        private readonly IRepository<ApplicantLogicTest> _logicTestRepository;
        private readonly IRepository<ApplicantProgrammingTest> _programmingTestRepository;
        private readonly IRepository<Role> _roleRepository;

        public HiringService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<HiringInterviewer> hiringInterviewerRepository, IRepository<SuperDictionary> superDictionaryRepository,
             IRepository<HiringData> hiringDataRepository, IRepository<ApplicantForeignLanguageTest> foreignLanguageTestRepository,
             IRepository<ApplicantLogicTest> logicTestRepository, IRepository<ApplicantProgrammingTest> programmingTestRepository,
             IRepository<HiringTestData> hiringTestDataRepository, IRepository<Role> roleRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _hiringInterviewerRepository = hiringInterviewerRepository;
            _superDictionaryRepository = superDictionaryRepository;
            _hiringDataRepository = hiringDataRepository;
            _foreignLanguageTestRepository = foreignLanguageTestRepository;
            _logicTestRepository = logicTestRepository;
            _programmingTestRepository = programmingTestRepository;
            _hiringTestDataRepository = hiringTestDataRepository;
            _roleRepository = roleRepository;
        }

        public List<HiringDataDTO> ListAll(int userId, string role, HiringFilterDTO filters)
        {
            var predicate = CreatePredicate(filters, out bool IsFilterActive);

            var totalElements = _hiringDataRepository.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<HiringDataDTO>();

            if (role == nameof(RoleEnum.Admin) || role == nameof(RoleEnum.HR))
            {
                return AutoMapperExpression.AutoMapHiringDataDTO(_hiringDataRepository.GetListResultSpec(x => x.Where(predicate))
                   .Include(x => x.Applicant)
                   .Include(x => x.Position)
                   .Include(x => x.Status)
                   .OrderBy(x => x.Applicant.NameEn).ThenBy(x => x.Applicant.SurnameEn));
            }
            else
            {
                var hiringDataIds = _hiringInterviewerRepository.GetListResultSpec(x => x.Where(p => p.InterviewerId == userId)).ToDictionary(k => k.HiringDataId, v => v.Id);
                predicate = predicate.And(p => hiringDataIds.ContainsKey(p.Id));
                return AutoMapperExpression.AutoMapHiringDataDTO(_hiringDataRepository.GetListResultSpec(x => x.Where(predicate))
                    .Include(x => x.Applicant)
                    .Include(x => x.Position)
                    .Include(x => x.Status)
                    .OrderBy(x => x.Applicant.NameEn).ThenBy(x => x.Applicant.SurnameEn));
            }
        }

        private Expression<Func<HiringData, bool>> CreatePredicate(HiringFilterDTO filters, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<HiringData>();
            IsFilterActive = false;

            if (!string.IsNullOrEmpty(filters.NameEn))
            {
                predicate = predicate.And(p => p.Applicant.NameEn.ToLower().Contains(filters.NameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.SurnameEn))
            {
                predicate = predicate.And(p => p.Applicant.SurnameEn.ToLower().Contains(filters.SurnameEn.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.Login))
            {
                predicate = predicate.And(p => p.Applicant.Login.ToLower() == filters.Login.ToLower());
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
                var statusIds = _superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.Name == nameof(HiringStatusEnum.Open)
                    && p.DictionaryId == (int)DictionaryEnum.HiringStatus)).Select(x => x.Id).ToList();

                predicate = predicate.And(p => statusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }

            return predicate;
        }

        public InterviewersDTO GetInterviewers()
        {
            var teamLeaderRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.TeamLeader))).First().Id;
            var lineManagerRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.LineManager))).First().Id;
            var hrRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.HR))).First().Id;
            var ceoRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.CEO))).First().Id;
            var chiefAccountantRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.ChiefAccountant))).First().Id;
            var baRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.BA))).First().Id;
            var adminRole = _roleRepository.GetResultSpec(x => x.Where(p => p.Title == nameof(RoleEnum.Admin))).First().Id;

            var teamLeaders = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetListResultSpec(x => x.Where(p => p.RoleId == teamLeaderRole))
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
            var lineManagers = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetListResultSpec(x => x.Where(p => p.RoleId == lineManagerRole))
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
            var hrs = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetListResultSpec(x => x.Where(p => p.RoleId == hrRole))
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
            var ceo = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetResultSpec(x => x.Where(p => p.RoleId == ceoRole).FirstOrDefault()));
            var chiefAccountant = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetResultSpec(x => x.Where(p => p.RoleId == chiefAccountantRole).FirstOrDefault()));
            var bas = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetListResultSpec(x => x.Where(p => p.RoleId == baRole))
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
            var admins = AutoMapperExpression.AutoMapShortDTO(_employeeRepository.GetListResultSpec(x => x.Where(p => p.RoleId == adminRole))
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));

            var interviewers = new InterviewersDTO
            {
                TeamLeaders = teamLeaders,
                LineManagers = lineManagers,
                HRs = hrs,
                CEO = ceo,
                ChiefAccountant = chiefAccountant,
                BAs = bas,
                Admins = admins,
            };

            return interviewers;
        }

        public HiringTestDTO ListTestShort(int id)
        {
            var foreignTest = AutoMapperExpression.AutoMapShortDTO(_foreignLanguageTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                .Include(x => x.ForeignLanguage)
                .OrderBy(x => x.ForeignLanguage)
                .OrderByDescending(x => x.Date));

            var logicTest = AutoMapperExpression.AutoMapShortDTO(_logicTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                .OrderByDescending(x => x.Date));

            var programmingTest = AutoMapperExpression.AutoMapShortDTO(_programmingTestRepository.GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                .Include(x => x.ProgrammingLanguage)
                .OrderBy(x => x.ProgrammingLanguage)
                .OrderByDescending(x => x.Date));

            var tests = new HiringTestDTO
            {
                ForeignTest = foreignTest,
                LogicTest = logicTest,
                ProgrammingTest = programmingTest,
            };

            return tests;
        }

        public List<ShortDTO> GetInterviewerPositions()
        {
            return AutoMapperExpression.AutoMapShortDTO(_roleRepository.GetListResultSpec(x => x.Where(p => (p.Title != nameof(RoleEnum.Accountant)) && (p.Title != nameof(RoleEnum.Developer)))));
        }

        public bool IsExistsActiveData(int applicantId)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus &&
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;
            return _hiringDataRepository.GetResultSpec(x => x.Any(p => p.ApplicantId == applicantId && p.StatusId == statusId));
        }

        public bool IsExistsData(int id) => _hiringDataRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        public bool IsExistsInterviewData(int id) => _hiringInterviewerRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        public bool CheckCreateData(HiringCreateDTO model)
        {
            bool foreignLanguage = true;
            bool logic = true;
            bool programming = true;

            if (model.ForeignLanguageTestId.HasValue)
            {
                if (!_foreignLanguageTestRepository.GetResultSpec(x => x.Any(p => (int)model.ForeignLanguageTestId == p.Id)))
                    foreignLanguage = false;
            }

            if (model.LogicTestId.HasValue)
            {
                if (!_logicTestRepository.GetResultSpec(x => x.Any(p => (int)model.LogicTestId == p.Id)))
                    logic = false;
            }

            if (model.ProgrammingTestId.HasValue)
            {
                if (!_programmingTestRepository.GetResultSpec(x => x.Any(p => (int)model.ProgrammingTestId == p.Id)))
                    programming = false;
            }

            var employees = _employeeRepository.GetListResultSpec(x => x).Select(x => x.Id);

            if (_applicantReporitory.GetResultSpec(x => x.Any(p => p.Id == model.ApplicantId)) &&
                _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.Id == model.PositionId)) &&
                model.Interviewers.All(x => employees.Contains(x)) && foreignLanguage && logic && programming)
                return false;
            else
                return true;
        }

        public HiringDTO GetById(int id)
        {
            var data = AutoMapperExpression.AutoMapHiringDTO(_hiringDataRepository.GetResultSpec(x => x.Where(p => p.Id == id)
                .Include(x => x.Applicant)
                .Include(x => x.Status)
                .Include(x => x.Position))
                .First());

            var testData = _hiringTestDataRepository.GetResultSpec(x => x.Where(p => p.HiringDataId == id)
                .Include(x => x.LogicTest)
                .Include(x => x.ProgrammingTest).ThenInclude(x => x.ProgrammingLanguage)
                .Include(x => x.ForeignLanguageTest).ThenInclude(x => x.ForeignLanguage)
                .First());

            data.ForeignLanguageTest = AutoMapperExpression.AutoMapShortDTO(testData.ForeignLanguageTest);
            data.ProgrammingTest = AutoMapperExpression.AutoMapShortDTO(testData.ProgrammingTest);
            data.LogicTest = AutoMapperExpression.AutoMapShortDTO(testData.LogicTest);

            data.Interviewers = AutoMapperExpression.AutoMapHiringInterviewerDTO(
                _hiringInterviewerRepository.GetListResultSpec(x => x.Where(p => p.HiringDataId == id)
                    .Include(x => x.Interviewer)
                    .Include(x => x.Position)));

            return data;
        }

        public HiringAcceptDTO GetApplicantHiringData(int id)
        {
            var hiring = _hiringDataRepository.GetById(id);
            var data = AutoMapperExpression.AutoMapHiringAcceptDTO(_applicantReporitory.GetResultSpec(x => x.Where(p => p.Id == hiring.ApplicantId).First()));

            data.PositionId = hiring.PositionId;

            return data;
        }

        public void Create(HiringCreateDTO data)
        {
            var hitingStatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus &&
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;

            var hiringData = new HiringData
            {
                ApplicantId = data.ApplicantId,
                Date = data.Date,
                PositionId = data.PositionId,
                StatusId = hitingStatusId
            };

            var hiringDataId = _hiringDataRepository.Create(hiringData).Id;

            var hiringTestData = new HiringTestData
            {
                ForeignLanguageTestId = data.ForeignLanguageTestId,
                ProgrammingTestId = data.ProgrammingTestId,
                LogicTestId = data.LogicTestId,
                HiringDataId = hiringDataId
            };

            _hiringTestDataRepository.Create(hiringTestData);

            foreach (var item in data.Interviewers)
            {
                var positionId = _employeeRepository.GetResultSpec(x => x.Where(p => p.Id == item).First()).RoleId;

                var hiringInterviewer = new HiringInterviewer
                {
                    InterviewerId = item,
                    Description = null,
                    HiringDataId = hiringDataId,
                    PositionId = positionId.Value,
                };

                _hiringInterviewerRepository.Create(hiringInterviewer);
            }

            var applicant = _applicantReporitory.GetResultSpec(x => x.Where(p => p.Id == data.ApplicantId)).First();

            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus &&
                p.Name == nameof(ApplicantStatusEnum.InProgress))).First().Id;

            _applicantReporitory.Update(applicant);
        }

        public void UpdateDescription(int id, string description)
        {
            var data = _hiringInterviewerRepository.GetById(id);

            if (data != null)
            {
                data.Description = description;

                _hiringInterviewerRepository.Update(data);
            }
        }

        public void Hire(HiringAcceptDTO model)
        {
            var hiringData = _hiringDataRepository.GetById(model.Id);
            var applicant = _applicantReporitory.GetById(hiringData.ApplicantId);

            var employeeActiveStatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus && p.Name == nameof(EmployeeStatusEnum.Active))).First().Id;

            bool isEmployeeExist = _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == applicant.Login));

            if (isEmployeeExist)
            {
                var employee = _employeeRepository.GetResultSpec(x => x.Where(p => p.Login == applicant.Login)).First();
                employee.NameRu = model.NameRu;
                employee.SurnameRu = model.SurnameRu;
                employee.PatronymicRu = model.PatronymicRu;
                employee.NameEn = model.NameEn;
                employee.SurnameEn = model.SurnameEn;
                employee.Phone = model.Phone;
                employee.StatusId = employeeActiveStatusId;
                employee.LanguageId = model.LanguageId;
                employee.DepartmentId = model.DepartmentId;
                employee.PositionId = model.PositionId;

                _employeeRepository.Update(employee);
            }
            else
            {
                var employee = new Employee()
                {
                    NameRu = model.NameRu,
                    SurnameRu = model.SurnameRu,
                    PatronymicRu = model.PatronymicRu,
                    NameEn = model.NameEn,
                    SurnameEn = model.SurnameEn,
                    Login = applicant.Login,
                    Phone = model.Phone,
                    LanguageId = model.LanguageId,
                    DepartmentId = model.DepartmentId,
                    StatusId = employeeActiveStatusId,
                    PositionId = model.PositionId,
                };

                _employeeRepository.Create(employee);
            }

            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x =>
               x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Hired))).First().Id;

            hiringData.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus && p.Name == nameof(HiringStatusEnum.Closed))).First().Id;

            _hiringDataRepository.Update(hiringData);
            _applicantReporitory.Update(applicant);
        }

        public void Reject(int id)
        {
            var hiringData = _hiringDataRepository.GetById(id);
            var applicant = _applicantReporitory.GetById(hiringData.ApplicantId);

            hiringData.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus && p.Name == nameof(HiringStatusEnum.Closed))).First().Id;

            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Active))).First().Id;

            _hiringDataRepository.Update(hiringData);
            _applicantReporitory.Update(applicant);
        }
    }
}
