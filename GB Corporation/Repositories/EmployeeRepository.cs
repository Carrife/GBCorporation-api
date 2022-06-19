using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;

namespace GB_Corporation.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Employee entity)
        {
            _context.Add(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(Employee entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public IQueryable<Employee> GetAll()
        {
            var entities = _context.Employees;
            return entities;
        }

        public Employee GetByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(x => x.Email == email);
        }

        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Employee entity)
        {
            _context.Employees.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}
