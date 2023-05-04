using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ITestCompetenciesService
    {
        List<CompetenciesTestDTO> GetTestData(string docPath);
        void Complete(TestCompleteDTO model);
        List<ShortDTO> GetAll();
        void Create(TestCreateDTO model);
        List<TestStatusDTO> GetUserTests(int? id, TestProgressFilterDTO filters);
        List<TestResultDTO> GetUserResults(int? id, TestResultFilterDTO filters);
        bool IsDocExists(int competenceId);
        int GetTemplateId(int competenceId);
    }
}
