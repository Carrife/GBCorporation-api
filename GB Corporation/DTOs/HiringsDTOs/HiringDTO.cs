namespace GB_Corporation.DTOs
{
    public class HiringDTO : BaseDTO
    {
        public ShortDTO Applicant { get; set; }
        public string Date { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public List<HiringInterviewerDTO> Interviewers { get; set; }
        public ShortDTO ForeignLanguageTest { get; set; }
        public ShortDTO LogicTest { get; set; }
        public ShortDTO ProgrammingTest { get; set; }
    }
}
