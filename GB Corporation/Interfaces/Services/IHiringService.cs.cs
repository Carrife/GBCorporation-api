using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        List<ApplicantHiringDataDTO> ListAll();
        List<ShortDTO> ListForeignTestShort(int id);
        List<ShortDTO> ListLogicTestShort(int id);
        List<ShortDTO> ListProgrammingTestShort(int id);
        void CreateApplicantHiringData(ApplicantHiringDataDTO register);
        List<ApplicantHiringDataDTO> GetById(int id);
        void Update(ApplicantHiringDataDTO model);
        bool IsExistsData(int id);
        bool IsExistsActiveData(int applicantId);
        void Hire(HiringDTO id);
        void Reject(int id);
        bool IsExists(string login);
        bool IsExists(int id);
    }
}
