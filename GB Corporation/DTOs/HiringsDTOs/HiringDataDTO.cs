namespace GB_Corporation.DTOs
{
    public class HiringDataDTO : BaseDTO
    {
        public ShortDTO Applicant { get; set; }
        public DateTime Date { get; set; }
        public ShortDTO Position { get; set; }
        public ShortDTO Status { get; set; }
    }
}
