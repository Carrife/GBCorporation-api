namespace GB_Corporation.Models
{
    public class ApplicantHiringData : BaseEntity
    {
        public int? ForeignLanguageId { get; set; }
        public int ForeignLanguageResult { get; set; }
        public int? ProgrammingLanguageId { get; set; }
        public int ProgrammingLanguageResult { get; set; }
        public int? TeamLeaderId { get; set; }
        public string TeamLeaderDescription { get; set; }
        public int? LineManagerId { get; set; }
        public string LineManagerDescription { get; set; }
        public SuperDictionary ProgrammingLanguage { get; set; }
        public SuperDictionary ForeignLanguage { get; set; }
        public Employee TeamLeader { get; set; }
        public Employee LineManager { get; set; }
    }
}
