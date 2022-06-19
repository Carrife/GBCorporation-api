using GB_Corporation.DTOs.TestCompetenciesDTOs;

namespace GB_Corporation.DTOs.EmployeeDTOs
{
    public class EmployeeWithTestsDTO
    {
        public EmployeeDTO Employee { get; set; }
        public List<TestCompetenciesDTO> TestCompetencies { get; set; }
    }
}
