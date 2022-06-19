using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface ITestCompetenciesReporitory
    {
        void Create(TestCompetencies entity);
        List<TestCompetencies> GetAll();
        void Update(TestCompetencies entity);
        void Delete(TestCompetencies entity);
    }
}
