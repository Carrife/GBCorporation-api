using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly AppDbContext _context;

        public TemplateRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Template entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(Template entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public List<Template> GetAll()
        {
            var entities = _context.Templates.ToList();
            return entities;
        }

        public void Update(Template entity)
        {
            _context.Templates.Update(entity);
            _context.SaveChangesAsync();
        }

        public Template GetById(int id)
        {
            return _context.Templates.FirstOrDefault(x => x.Id == id);
        }
    }
}
