namespace GB_Corporation.DTOs
{
    public class HiringInterviewerDTO : BaseDTO
    {
        public ShortDTO Interviewer { get; set; }
        public string Description { get; set; }
        public ShortDTO Position { get; set; }
    }
}
