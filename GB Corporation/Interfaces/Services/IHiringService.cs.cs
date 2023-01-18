using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        bool IsExists(string login);
        void CreateApplicant(ApplicantDTO register);
        void CreateApplicantHiringData(ApplicantHiringDataDTO register);
        List<ApplicantDTO> ListAll();
        bool IsExists(int id);
        List<ApplicantHiringDataDTO> GetById(int id);
        void Update(ApplicantHiringDataDTO model);
        bool IsExistsData(int id);
        void Hire(HiringDTO id);
        void Reject(int id);
    }
}
