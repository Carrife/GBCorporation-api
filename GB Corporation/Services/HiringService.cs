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

        public HiringService(IRepository<Employee> employeeRepository, IRepository<Applicant> applicantReporitory,
             IRepository<ApplicantHiringData> applicantHiringDataRepository, IRepository<SuperDictionary> superDictionaryRepository)
        {
            _employeeRepository = employeeRepository;
            _applicantReporitory = applicantReporitory;
            _applicantHiringDataRepository = applicantHiringDataRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }
    }
}
