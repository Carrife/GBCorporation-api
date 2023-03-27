namespace GB_Corporation.Models
{
    public class ApplicantForeignLanguageTest : BaseEntity
    {
        public int ForeignLanguageId { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
        public SuperDictionary ForeignLanguage { get; set; }
        public Applicant Applicant { get; set; }
    }
}
