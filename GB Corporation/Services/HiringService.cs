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

        public bool IsExists(string login) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Login == login)) || _employeeRepository.GetResultSpec(x => x.Any(p => p.Login == login));

        public bool IsExists(int id) => _applicantReporitory.GetResultSpec(x => x.Any(p => p.Id == id));

        public bool IsExistsData(int id) => _applicantHiringDataRepository.GetResultSpec(x => x.Any(p => p.Id == id));

        public List<ApplicantHiringDataDTO> GetById(int id)
        {
            //var hiringDatas = _hiringDataRepository.GetResultSpec(x => x.Where(p => p.ApplicantId == id)).First().ApplicantHiringDataIds;

            /*var applicantData = AutoMapperExpression.AutoMapApplicantHiringDataDTO(
                _applicantHiringDataRepository.GetListResultSpec(x => x.Where(p => hiringDatas.Contains(p.Id))
                //.Include(p => p.ForeignLanguage)
                //.Include(p => p.ProgrammingLanguage)
                .Include(p => p.LineManager)
                .Include(p => p.TeamLeader)));

            return applicantData;*/
            return null;
        }

        public void CreateApplicantHiringData(ApplicantHiringDataDTO data)
        {
            var applicantHiringData = new ApplicantHiringData
            {
                //ForeignLanguageId = data.ForeignLanguage.Id,
                //ForeignLanguageResult = data.ForeignLanguageResult,
                //ProgrammingLanguageId = data.ProgrammingLanguage.Id,
                //ProgrammingLanguageResult = data.ProgrammingLanguageResult,
                TeamLeaderId = data.TeamLeader.Id,
                TeamLeaderDescription = data.TeamLeaderDescription,
                LineManagerId = data.LineManager.Id,
                LineManagerDescription = data.LineManagerDescription
            };

            var hiringDataId = _applicantHiringDataRepository.Create(applicantHiringData).Id;

            var hiringData = new HiringData
            {
                ApplicantId = data.ApplicantId,
                //ApplicantHiringDataIds = new List<int> { hiringDataId },
            };

            _hiringDataRepository.Create(hiringData);
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
