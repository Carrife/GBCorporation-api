using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;

namespace GB_Corporation.Services
{
    public class HiringService : IHiringService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IApplicantRepository _applicantReporitory;
        private readonly IApplicantHiringDataRepository _applicantHiringDataRepository;
        private readonly ISuperDictionaryRepository _superDictionaryRepository;

        public HiringService(IEmployeeRepository employeeRepository, IApplicantRepository applicantReporitory,
             IApplicantHiringDataRepository applicantHiringDataRepository, ISuperDictionaryRepository superDictionaryRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _applicantHiringDataRepository = applicantHiringDataRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }
    }
}
