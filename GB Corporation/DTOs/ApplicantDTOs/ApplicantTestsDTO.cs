namespace GB_Corporation.DTOs
{
    public class ApplicantTestsDTO
    {
        public List<ForeignLanguageTestDTO> ForeignLanguageTests { get; set; }
        public List<LogicTestDTO> LogicTests { get; set; }
        public List<ProgrammingTestDTO> ProgrammingTests { get; set; }
    }
}