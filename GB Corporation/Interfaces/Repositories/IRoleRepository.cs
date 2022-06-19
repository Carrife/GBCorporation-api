using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface IRoleRepository
    {
            void Create(Role entity);
            IQueryable<Role> GetAll();
            void Update(Role entity);
            void Delete(Role entity);
            Role GetById(int id);
    }
}
