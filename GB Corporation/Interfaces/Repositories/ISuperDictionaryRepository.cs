using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface ISuperDictionaryRepository
    {
        void Create(SuperDictionary entity);
        List<SuperDictionary> GetAll();
        void Update(SuperDictionary entity);
        void Delete(SuperDictionary entity);
    }
}
