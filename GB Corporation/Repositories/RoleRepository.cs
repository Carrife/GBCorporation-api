using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Role entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(Role entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public IQueryable<Role> GetAll()
        {
            var entities = _context.Roles;
            return entities;
        }

        public Role GetById(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Role entity)
        {
            _context.Roles.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}
