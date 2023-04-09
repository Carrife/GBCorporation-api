using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ITemplateService
    {
        bool IsExists(string name);
        List<TemplateDTO> GetAll();
        void Create(TemplateCreateDTO model);
        void Update(TemplateDTO model);
        void Delete(int id);
        bool IsExists(int id);
        void Upload(IFormFile file, int templateId);
        bool IsDocExists(int id);
        string GetFilePath(int templateId);
        string GetFileName(int templateId);
    }
}
