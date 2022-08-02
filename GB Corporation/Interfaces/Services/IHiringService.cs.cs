using GB_Corporation.DTOs.HiringsDTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        void CreateApplicant(ApplicantDTO register);
        void CreateApplicantHiringData(ApplicantHiringDataDTO register);
    }
}
