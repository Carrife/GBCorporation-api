using GB_Corporation.DTOs.TestCompetenciesDTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ITestCompetenciesService
    {
        List<CompetenciesTestDTO> GetTestData(string docPath);
        void Complete(TestCompleteDTO model);
    }
}
