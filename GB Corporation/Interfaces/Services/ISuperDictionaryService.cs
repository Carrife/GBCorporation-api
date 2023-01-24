using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ISuperDictionaryService
    {
        List<ShortDTO> GetProgrammingLanguages();
        List<ShortDTO> GetDepartments();
        List<ShortDTO> GetForeignLanguages();
        void Create(LogicTestDTO model);
        void Create(ForeignLanguageTestDTO model);
        void Create(ProgrammingTestDTO model);
    }
}
