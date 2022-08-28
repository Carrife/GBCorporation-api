namespace GB_Corporation.DTOs
{
    public class ApplicantHiringDataDTO : BaseDTO
    {
        public int ApplicantId { get; set; }
        public ShortDTO ForeignLanguage { get; set; }
        public int? ForeignLanguageResult { get; set; }
        public ShortDTO ProgrammingLanguage { get; set; }
        public int? ProgrammingLanguageResult { get; set; }
        public ShortDTO TeamLeader { get; set; }
        public string? TeamLeaderDescription { get; set; }
        public ShortDTO LineManager { get; set; }
        public string? LineManagerDescription { get; set; }
    }
}
