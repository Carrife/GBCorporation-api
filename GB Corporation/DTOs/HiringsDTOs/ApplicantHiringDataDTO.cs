namespace GB_Corporation.DTOs.HiringsDTOs
{
    public class ApplicantHiringDataDTO
    {
        public int? ApplicantId { get; set; }
        public int? ForeignLanguageId { get; set; }
        public int ForeignLanguageResult { get; set; }
        public int? ProgrammingLanguageId { get; set; }
        public int? ProgrammingLanguageResult { get; set; }
        public int? TeamLeaderId { get; set; }
        public string? TeamLeaderDescription { get; set; }
        public int? LineManagerId { get; set; }
        public string? LineManagerDescription { get; set; }
    }
}
