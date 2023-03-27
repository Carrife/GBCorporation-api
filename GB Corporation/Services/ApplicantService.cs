using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<ApplicantDTO> ListAll()
        {
            return AutoMapperExpression.AutoMapApplicantDTO(_applicantReporitory.GetListResultSpec(x => x)
                    .Include(x => x.Status)
                    .OrderBy(x => x.NameEn).ThenBy(x => x.SurnameEn));
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
                Date = model.Date.ToUniversalTime(),
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
                Date = model.Date.ToUniversalTime(),
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
                Date = model.Date.ToUniversalTime(),
                ApplicantId = model.ApplicantId
            };

            _applicantProgrammingTestRepository.Create(test);
        }
    }
}
