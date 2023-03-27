namespace GB_Corporation.Models
{
    public class ApplicantEmployee : BaseEntity
    {
        public int? ApplicantId { get; set; }
        public int? EmployeeId { get; set; }
        public Applicant Applicant { get; set; }
        public Employee Employee { get; set; }
    }
}
