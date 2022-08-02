using GB_Corporation.DTOs.HiringsDTOs;
using GB_Corporation.Enums;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class HiringService : IHiringService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Applicant> _applicantReporitory;
        private readonly IRepository<ApplicantHiringData> _applicantHiringDataRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<HiringData> _hiringDataRepository;

        public HiringService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<ApplicantHiringData> applicantHiringDataRepository, IRepository<SuperDictionary> superDictionaryRepository,
             IRepository<HiringData> hiringDataRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _applicantHiringDataRepository = applicantHiringDataRepository;
            _superDictionaryRepository = superDictionaryRepository;
            _hiringDataRepository = hiringDataRepository;
        }

        public void CreateApplicant(ApplicantDTO register)
        {
            var applicant = new Applicant
            {
                NameEn = register.NameEn,
                SurnameEn = register.SurnameEn,
                PatronymicRu = register.PatronymicRu,
                NameRu = register.NameRu,
                SurnameRu = register.SurnameRu,
                Login = register.Login,
                Phone = register.Phone,
                StatusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ApplicantStatus && p.Name == nameof(ApplicantStatusEnum.Active))).First().Id,
            };

            _applicantReporitory.Create(applicant);
        }

        public void CreateApplicantHiringData(ApplicantHiringDataDTO data)
        {
            var applicantHiringData = new ApplicantHiringData
            {
                ForeignLanguageId = data.ForeignLanguageId,
                ForeignLanguageResult = data.ForeignLanguageResult,
                ProgrammingLanguageId = data.ProgrammingLanguageId,
                ProgrammingLanguageResult = data.ProgrammingLanguageResult,
            };

            var hiringDataId = _applicantHiringDataRepository.Create(applicantHiringData).Id;

            var hiringData = new HiringData
            {
                ApplicantId = data.ApplicantId,
                ApplicantHiringDataIds = new List<int> { hiringDataId },
            };

            _hiringDataRepository.Create(hiringData);
        }
    }
}
