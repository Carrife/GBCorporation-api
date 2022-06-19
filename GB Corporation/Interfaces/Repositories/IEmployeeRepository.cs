using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        void Create(Employee entity);
        IQueryable<Employee> GetAll();
        void Update(Employee entity);
        void Delete(Employee entity);
        Employee GetByEmail(string email);
        Employee GetById(int id);
    }
}
