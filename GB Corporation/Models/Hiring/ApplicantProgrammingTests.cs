namespace GB_Corporation.Models
{
    public class ApplicantProgrammingTest : BaseEntity
    {
        public int ProgrammingLanguageId { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public SuperDictionary ProgrammingLanguage { get; set; }
    }
}
