using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ISuperDictionaryService
    {
        List<ShortDTO> GetProgrammingLanguages();
        List<ShortDTO> GetDepartments();
        List<ShortDTO> GetForeignLanguages();
        List<ShortDTO> GetPositions();
    }
}
