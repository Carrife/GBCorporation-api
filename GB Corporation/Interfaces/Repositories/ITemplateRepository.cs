using GB_Corporation.Models;

namespace GB_Corporation.Interfaces.Repositories
{
    public interface ITemplateRepository
    {
        void Create(Template entity);
        List<Template> GetAll();
        void Update(Template entity);
        void Delete(Template entity);
        Template GetById(int id);
    }
}
