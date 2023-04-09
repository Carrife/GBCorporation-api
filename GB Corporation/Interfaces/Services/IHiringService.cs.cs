using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IHiringService
    {
        List<HiringDataDTO> ListAll(int userId, string role);
        InterviewersDTO GetInterviewers();
        HiringTestDTO ListTestShort(int id);
        List<ShortDTO> GetInterviewerPositions();
        List<ShortDTO> GetPositions();
        void Create(HiringCreateDTO data);
        HiringDTO GetById(int id);
        HiringAcceptDTO GetApplicantHiringData(int id);
        void UpdateDescription(int id, string description);
        bool IsExistsData(int id);
        public bool IsExistsInterviewData(int id);
        bool IsExistsActiveData(int applicantId);
        bool CheckCreateData(HiringCreateDTO model);
        void Hire(HiringAcceptDTO id);
        void Reject(int id);
    }
}
