using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ITestCompetenciesService
    {
        List<CompetenciesTestDTO> GetTestData(string docPath);
        void Complete(TestCompleteDTO model);
        List<TemplateDTO> GetAll();
        List<TestCompetenciesDTO> GetUserTests(int id);
    }
}
