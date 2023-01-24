using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IApplicantService
    {
        List<ApplicantDTO> ListAll();
        void Create(ApplicantCreateDTO register);
        bool IsExists(string login);
        bool IsExists(int id);
        void Update(ApplicantUpdateDTO model);
        ApplicantTestsDTO GetTestDatas(int id);
    }
}
