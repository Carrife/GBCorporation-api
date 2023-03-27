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
        private readonly IRepository<HiringInterviewer> _hiringInterviewerRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<HiringData> _hiringDataRepository;
        private readonly IRepository<HiringTestData> _hiringTestDataRepository;
        private readonly IRepository<ApplicantForeignLanguageTest> _foreignLanguageTestRepository;
        private readonly IRepository<ApplicantLogicTest> _logicTestRepository;
        private readonly IRepository<ApplicantProgrammingTest> _programmingTestRepository;

        public HiringService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<HiringInterviewer> hiringInterviewerRepository, IRepository<SuperDictionary> superDictionaryRepository,
             IRepository<HiringData> hiringDataRepository, IRepository<ApplicantForeignLanguageTest> foreignLanguageTestRepository,
             IRepository<ApplicantLogicTest> logicTestRepository, IRepository<ApplicantProgrammingTest> programmingTestRepository,
             IRepository<HiringTestData> hiringTestDataRepository)
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
        }

        public List<HiringDataDTO> ListAll(int userId, string role)
        {
            if(role == nameof(RoleEnum.Admin) || role == nameof(RoleEnum.HR))
            {
                return AutoMapperExpression.AutoMapHiringDataDTO(_hiringDataRepository.GetListResultSpec(x => x)
                   .Include(x => x.Applicant)
                   .Include(x => x.Position)
                   .Include(x => x.Status)
                   .OrderBy(x => x.Date));
            }
            else
            {
               var hiringDataIds = _hiringInterviewerRepository.GetListResultSpec(x => x.Where(p => p.InterviewerId == userId)).ToDictionary(k => k.HiringDataId, v => v.Id);
               return AutoMapperExpression.AutoMapHiringDataDTO(_hiringDataRepository.GetListResultSpec(x => x.Where(p => hiringDataIds.ContainsKey(p.Id)))
                   .Include(x => x.Applicant)
                   .Include(x => x.Position)
                   .Include(x => x.Status)
                   .OrderBy(x => x.Date));
            }
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

        public bool IsExistsActiveData(int applicantId)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus && 
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;
            return _hiringDataRepository.GetResultSpec(x => x.Any(p => p.ApplicantId == applicantId && p.StatusId == statusId));
        }

        public bool IsExistsData(int id) => _hiringDataRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        public bool IsExistsInterviewData(int id) => _hiringInterviewerRepository.GetResultSpec(x => x.Any(p => p.Id == id));

        public HiringDTO GetById(int id)
        {
            var data = AutoMapperExpression.AutoMapHiringDTO(_hiringDataRepository.GetResultSpec(x => x.Where(p => p.Id == id).First()));
            
            var testData = _hiringTestDataRepository.GetResultSpec(x => x.Where(p => p.HiringDataId == id).First());

            data.ForeignLanguageTest = AutoMapperExpression.AutoMapApplicantForeignLanguageTestDTO(testData.ForeignLanguageTest);
            data.ProgrammingTest = AutoMapperExpression.AutoMapApplicantProgrammingTestDTO(testData.ProgrammingTest);
            data.LogicTest = AutoMapperExpression.AutoMapApplicantLogicTestDTO(testData.LogicTest);

            data.Interviewers = AutoMapperExpression.AutoMapHiringInterviewerDTO(
                _hiringInterviewerRepository.GetListResultSpec(x => x.Where(p => p.HiringDataId == id)
                    .Include(x => x.Interviewer)
                    .Include(x => x.Position)));
            
            return data;
        }

        public void CreateHiringData(HiringDTO data)
        {
            var hitingStatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.HiringStatus &&
                p.Name == nameof(HiringStatusEnum.Open))).First().Id;

            var hiringData = new HiringData
            {
                ApplicantId = data.Applicant.Id,
                Date = data.Date,
                PositionId = data.Position.Id,
                StatusId = hitingStatusId
            };
            
            var hiringDataId = _hiringDataRepository.Create(hiringData).Id;

            var hiringTestData = new HiringTestData
            {
                ForeignLanguageTestId = data.ForeignLanguageTest.Id,
                ProgrammingTestId = data.ProgrammingTest.Id,
                LogicTestId = data.LogicTest.Id,
                HiringDataId = hiringDataId
            };

            _hiringTestDataRepository.Create(hiringTestData);

            foreach (var item in data.Interviewers)
            {
                var hiringInterviewer = new HiringInterviewer
                {
                    InterviewerId = item.Interviewer.Id,
                    Description = null,
                    HiringDataId = hiringDataId,
                    PositionId = item.Position.Id,
                };

                _hiringInterviewerRepository.Create(hiringInterviewer);
            }

            var applicant = _applicantReporitory.GetResultSpec(x => x.Where(p => p.Id == data.Applicant.Id)).First();
            
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
            var applicant = _applicantReporitory.GetById(model.ApplicantId);
            
            applicant.StatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Hired))).First().Id;

            var employeeActiveStatusId = _superDictionaryRepository.GetResultSpec(x =>
                x.Where(p => p.DictionaryId == (int)DictionaryEnum.EmployeeStatus && p.Name == nameof(EmployeeStatusEnum.Active))).First().Id;

            bool isEmployeeExist = _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == applicant.Login));

            if(isEmployeeExist)
            {
                var employee = _employeeRepository.GetResultSpec(x => x.Where(p => p.Login == applicant.Login)).First();
                employee.NameRu = applicant.NameRu;
                employee.SurnameRu = applicant.SurnameRu;
                employee.PatronymicRu = applicant.PatronymicRu;
                employee.NameEn = applicant.NameEn;
                employee.SurnameEn = applicant.SurnameEn;
                employee.Phone = applicant.Phone;
                employee.StatusId = employeeActiveStatusId;
                employee.LanguageId = model.LanguageId;
                employee.DepartmentId = model.DepartmentId;
                //employee.Position = model.Position;

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
                    LanguageId = model.LanguageId,
                    DepartmentId = model.DepartmentId,
                    StatusId = employeeActiveStatusId
                    //Position = model.Position
                };

                _employeeRepository.Create(employee);
            }

            var hiringData = _hiringDataRepository.GetById(model.Id);

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
