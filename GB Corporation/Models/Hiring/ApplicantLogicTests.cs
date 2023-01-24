namespace GB_Corporation.Models
{
    public class ApplicantLogicTest : BaseEntity
    {
        public int Result { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
