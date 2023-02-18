using Microsoft.EntityFrameworkCore;
using Npgsql;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;
using GB_Corporation.Data;

namespace GB_Corporation.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context) => _context = context;

        public T Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            var local = _context.Set<T>().Local.FirstOrDefault(l => l.Id.Equals(entity.Id));
            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public string Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return null;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pex)
            {
                return pex.TableName;
            }
        }

        public T GetById(int id) => _context.Set<T>().SingleOrDefault(x => x.Id == id);

        public IQueryable<T1> GetListResultSpec<T1>(Func<IQueryable<T>, IQueryable<T1>> func) => func(_context.Set<T>().AsNoTracking());
        public T1 GetResultSpec<T1>(Func<IQueryable<T>, T1> func) => func(_context.Set<T>().AsNoTracking());
    }
}
