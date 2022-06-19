using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Repositories
{
    public class TestCompetenciesRepository : ITestCompetenciesReporitory
    {
        private readonly AppDbContext _context;

        public TestCompetenciesRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(TestCompetencies entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(TestCompetencies entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public List<TestCompetencies> GetAll()
        {
            var entities = _context.TestCompetencies.ToList();
            return entities;
        }

        public void Update(TestCompetencies entity)
        {
            _context.TestCompetencies.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}
