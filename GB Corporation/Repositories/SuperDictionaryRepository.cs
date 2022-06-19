using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Repositories
{
    public class SuperDictionaryRepository : ISuperDictionaryRepository
    {
        private readonly AppDbContext _context;

        public SuperDictionaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(SuperDictionary entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(SuperDictionary entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public List<SuperDictionary> GetAll()
        {
            var entities = _context.SuperDictionaries.ToList();
            return entities;
        }

        public void Update(SuperDictionary entity)
        {
            _context.SuperDictionaries.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}
