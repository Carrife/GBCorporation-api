using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        void CreateApplicantHiringData(ApplicantHiringDataDTO register);
        List<ApplicantHiringDataDTO> GetById(int id);
        void Update(ApplicantHiringDataDTO model);
        bool IsExistsData(int id);
        void Hire(HiringDTO id);
        void Reject(int id);
        bool IsExists(string login);
        bool IsExists(int id);
    }
}
