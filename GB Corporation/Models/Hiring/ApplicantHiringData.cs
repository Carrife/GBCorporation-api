namespace GB_Corporation.Models
{
    public class ApplicantHiringData : BaseEntity
    {
        public int ApplicantId { get; set; }
        public int ForeignLanguageTestId { get; set; }
        public int LogicTestId { get; set; }
        public int ProgrammingTestId { get; set; }
        public int TeamLeaderId { get; set; }
        public string? TeamLeaderDescription { get; set; }
        public int LineManagerId { get; set; }
        public string? LineManagerDescription { get; set; }
        public DateTime Date { get; set; }
        public Applicant Applicant { get; set; }
        public Employee TeamLeader { get; set; }
        public Employee LineManager { get; set; }
        public ApplicantForeignLanguageTest ForeignLanguageTest { get; set; }
        public ApplicantLogicTest LogicTest { get; set; }
        public ApplicantProgrammingTest ProgrammingTest { get; set; }
    }
}
