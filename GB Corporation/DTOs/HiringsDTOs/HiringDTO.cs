namespace GB_Corporation.DTOs
{
    public class HiringDTO : BaseDTO
    {
        public ShortDTO Applicant { get; set; }
        public DateTime Date { get; set; }
        public ShortDTO Position { get; set; }
        public ShortDTO Status { get; set; }
        public List<HiringInterviewerDTO> Interviewers { get; set; }
        public ForeignLanguageTestDTO ForeignLanguageTest { get; set; }
        public LogicTestDTO LogicTest { get; set; }
        public ProgrammingTestDTO ProgrammingTest { get; set; }
    }
}
