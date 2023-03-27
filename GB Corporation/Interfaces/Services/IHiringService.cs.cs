using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        List<HiringDataDTO> ListAll(int userId, string role);
        List<ShortDTO> ListForeignTestShort(int id);
        List<ShortDTO> ListLogicTestShort(int id);
        List<ShortDTO> ListProgrammingTestShort(int id);
        void CreateHiringData(HiringDTO register);
        HiringDTO GetById(int id);
        void UpdateDescription(int id, string description);
        bool IsExistsData(int id);
        public bool IsExistsInterviewData(int id);
        bool IsExistsActiveData(int applicantId);
        void Hire(HiringAcceptDTO id);
        void Reject(int id);
    }
}
