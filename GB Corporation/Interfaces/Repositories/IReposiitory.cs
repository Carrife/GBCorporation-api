using GB_Corporation.Models;
using System.Linq.Expressions;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T Update(T entity);
        IEnumerable<T> UpdateRange(List<T> entities);
        string Delete(T entity);
        string DeleteRange(List<T> entities);
        IEnumerable<T> ListAll();
        T GetById(int id);
        IQueryable<T1> GetListResultSpec<T1>(Func<IQueryable<T>, IQueryable<T1>> func);
        T1 GetResultSpec<T1>(Func<IQueryable<T>, T1> func);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        IEnumerable<T> CreateMany(IEnumerable<T> entities);
        void DeleteById(int id);
    }
}
