namespace GB_Corporation.Models
{
    public class HiringData : BaseEntity
    {
        public int ApplicantId { get; set; }
        public DateTime Date { get; set; }
        public int PositionId { get; set; }
        public int StatusId { get; set; }
        public Applicant Applicant { get; set; }
        public SuperDictionary Status { get; set; }
        public SuperDictionary Position { get; set; }
    }
}
