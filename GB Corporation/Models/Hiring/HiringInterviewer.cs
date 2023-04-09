namespace GB_Corporation.Models
{
    public class HiringInterviewer : BaseEntity
    {
        public int InterviewerId { get; set; }
        public string? Description { get; set; }
        public int PositionId { get; set; }
        public int HiringDataId { get; set; }
        public Employee Interviewer { get; set; }
        public HiringData HiringData { get; set; }
        public Role Position { get; set; }
    }
}
