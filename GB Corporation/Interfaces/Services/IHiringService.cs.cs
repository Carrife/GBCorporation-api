using GB_Corporation.DTOs.HiringsDTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        bool IsExists(string login);
        void CreateApplicant(ApplicantDTO register);
        void CreateApplicantHiringData(ApplicantHiringDataDTO register);
    }
}
