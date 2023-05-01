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
    public class ApplicantService : IApplicantService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantReporitory;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<ApplicantLogicTest> _applicantLogicTestsRepository;
        private readonly IRepository<ApplicantProgrammingTest> _applicantProgrammingTestRepository;
        private readonly IRepository<ApplicantForeignLanguageTest> _applicantForeignLanguageTestRepository;

        public ApplicantService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<SuperDictionary> superDictionaryRepository, IRepository<ApplicantLogicTest> applicantLogicTestsRepository, 
             IRepository<ApplicantProgrammingTest> applicantProgrammingTestRepository,
             IRepository<ApplicantForeignLanguageTest> applicantForeignLanguageTestRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _superDictionaryRepository = superDictionaryRepository;
            _applicantForeignLanguageTestRepository = applicantForeignLanguageTestRepository;
            _applicantLogicTestsRepository = applicantLogicTestsRepository;
            _applicantProgrammingTestRepository = applicantProgrammingTestRepository;
        }

        public List<ApplicantDTO> ListAll(ApplicantFilterDTO filters)
        {
            var predicate = CreatePredicate(filters, out bool IsFilterActive);

            var totalElements = _applicantReporitory.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<ApplicantDTO>();

            return AutoMapperExpression.AutoMapApplicantDTO(_applicantReporitory.GetListResultSpec(x => x.Where(predicate))
                    .Include(x => x.Status)
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
        }

        private Expression<Func<Applicant, bool>> CreatePredicate(ApplicantFilterDTO filters, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<Applicant>();
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

            if (filters.StatusIds != null && filters.StatusIds.Length > 0)
            {
                predicate = predicate.And(p => filters.StatusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }
            else
            {
                var statusIds = _superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.Name != nameof(ApplicantStatusEnum.Hired)
                    && p.DictionaryId == (int)DictionaryEnum.ApplicantStatus)).Select(x => x.Id).ToList();

                predicate = predicate.And(p => statusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }

            return predicate;
        }

        public ApplicantUpdateDTO GetById(int id)
        {
            return AutoMapperExpression.AutoMapApplicantUpdateDTO(_applicantReporitory.GetById(id));
        }

        public List<ShortDTO> ListActiveShort()
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus &&
                p.Name == nameof(ApplicantStatusEnum.Active))).First().Id;
            return AutoMapperExpression.AutoMapShortDTO(_applicantReporitory.GetListResultSpec(x => x.Where(p => p.StatusId == statusId))
                .Include(x => x.Status)
                .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
        }

        public void Create(ApplicantCreateDTO model)
        {
            var applicant = new Applicant
            {
                NameEn = model.NameEn,
                SurnameEn = model.SurnameEn,
                PatronymicRu = model.PatronymicRu,
                NameRu = model.NameRu,
                SurnameRu = model.SurnameRu,
                Login = model.Login,
                Phone = model.Phone,
                StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(EmployeeStatusEnum.Active))).First().Id,
            };

            _applicantReporitory.Create(applicant);
        }

        public void Update(ApplicantUpdateDTO model)
        {
            var data = _applicantReporitory.GetById(model.Id);

            if (data != null)
            {
                data.NameEn = model.NameEn;
                data.SurnameEn = model.SurnameEn;
                data.NameRu = model.NameRu;
                data.SurnameRu = model.SurnameRu;
                data.PatronymicRu = model.PatronymicRu;
                data.Phone = model.Phone;

                _applicantReporitory.Update(data);
            }
        }

        public bool IsExists(string login) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Login == login)) || _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == login));
        public bool IsExists(int id) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Id == id));

        public ApplicantTestsDTO GetTestDatas(int id)
        {           
            var data = new ApplicantTestsDTO
            {
                LogicTests = AutoMapperExpression.AutoMapApplicantLogicTestDTO(_applicantLogicTestsRepository
                    .GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                        .OrderByDescending(x => x.Date)),
                ForeignLanguageTests = AutoMapperExpression.AutoMapApplicantForeignLanguageTestDTO(_applicantForeignLanguageTestRepository
                    .GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                        .Include(x => x.ForeignLanguage)
                        .OrderByDescending(x => x.Date)),
                ProgrammingTests = AutoMapperExpression.AutoMapApplicantProgrammingTestDTO(_applicantProgrammingTestRepository
                    .GetListResultSpec(x => x.Where(p => p.ApplicantId == id))
                        .Include(x => x.ProgrammingLanguage)
                        .OrderByDescending(x => x.Date))
            };

            return data;
        }

        public void CreateTestData(LogicTestDTO model)
        {
            var test = new ApplicantLogicTest
            {
                Result = model.Result,
                Date = DateTime.Parse(model.Date).ToUniversalTime(),
                ApplicantId = model.ApplicantId
            };

            _applicantLogicTestsRepository.Create(test);
        }

        public void CreateTestData(ForeignLanguageTestDTO model)
        {
            var test = new ApplicantForeignLanguageTest
            {
                ForeignLanguageId = _superDictionaryRepository.GetResultSpec(x => x.Where(p =>
                    p.DictionaryId == (int)DictionaryEnum.ForeignLanguage && p.Id == model.ForeignLanguageId).First()).Id,
                Result = model.Result,
                Date = DateTime.Parse(model.Date).ToUniversalTime(),
                ApplicantId = model.ApplicantId
            };

            _applicantForeignLanguageTestRepository.Create(test);
        }

        public void CreateTestData(ProgrammingTestDTO model)
        {
            var test = new ApplicantProgrammingTest
            {
                ProgrammingLanguageId = _superDictionaryRepository.GetResultSpec(x => x.Where(p =>
                    p.DictionaryId == (int)DictionaryEnum.ProgrammingLanguage && p.Id == model.ProgrammingLanguageId).First()).Id,
                Result = model.Result,
                Date = DateTime.Parse(model.Date).ToUniversalTime(),
                ApplicantId = model.ApplicantId
            };

            _applicantProgrammingTestRepository.Create(test);
        }
    }
}
