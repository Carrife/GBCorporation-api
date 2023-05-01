using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T Update(T entity);
        string Delete(T entity);
        T GetById(int id);
        IQueryable<T1> GetListResultSpec<T1>(Func<IQueryable<T>, IQueryable<T1>> func);
        T1 GetResultSpec<T1>(Func<IQueryable<T>, T1> func);
    }
}
