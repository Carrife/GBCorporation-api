using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface ISuperDictionaryService
    {
        List<ShortDTO> GetProgrammingLanguages();
        List<ShortDTO> GetDepartments();
        List<ShortDTO> GetForeignLanguages();
        List<ShortDTO> GetPositions();
        List<ShortDTO> GetEmployeeStatuses();
        List<ShortDTO> GetApplicantStatuses();
        List<ShortDTO> GetHiringStatuses();
        List<ShortDTO> GetTestCompetenciesStatuses();
        bool IsPositionExists(string name);
        void CreatePosition(ShortDTO model);
        bool IsExists(int id);
        void DeletePosition(int id);
        void UpdatePosition(ShortDTO model);
        bool IsProgrammingLanguageExists(string name);
        void CreateProgrammingLanguage(ShortDTO model);
        void DeleteProgrammingLanguage(int id);
        void UpdateProgrammingLanguage(ShortDTO model);
        bool IsDepartmentExists(string name);
        void CreateDepartment(ShortDTO model);
        void DeleteDepartment(int id);
        void UpdateDepartment(ShortDTO model);
        bool IsForeignLanguageExists(string name);
        void CreateForeignLanguage(ShortDTO model);
        void DeleteForeignLanguage(int id);
        void UpdateForeignLanguage(ShortDTO model);
    }
}
