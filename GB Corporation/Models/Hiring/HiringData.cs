namespace GB_Corporation.Models
{
    public class HiringData
    {
        public int Id { get; set; }
        public int? ApplicantId { get; set; }
        public int? EmployeeId { get; set; }
        public List<int> ApplicantHiringDataIds { get; set; }
        public Applicant Applicant { get; set; }
        public Employee Employee { get; set; }
    }
}
