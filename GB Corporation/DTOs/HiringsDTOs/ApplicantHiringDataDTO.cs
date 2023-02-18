namespace GB_Corporation.DTOs
{
    public class ApplicantHiringDataDTO : BaseDTO
    {
        public ShortDTO Applicant { get; set; }
        public HiringTestDTO ForeignLanguageTest { get; set; }
        public HiringTestDTO LogicTest { get; set; }
        public HiringTestDTO ProgrammingTest { get; set; }
        public DateTime Date { get; set; }
        public ShortDTO TeamLeader { get; set; }
        public string? TeamLeaderDescription { get; set; }
        public ShortDTO LineManager { get; set; }
        public string? LineManagerDescription { get; set; }
        public ShortDTO Status { get; set; }
    }
}
