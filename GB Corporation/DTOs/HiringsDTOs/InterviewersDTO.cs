namespace GB_Corporation.DTOs
{
    public class InterviewersDTO
    {
        public List<ShortDTO> TeamLeaders { get; set; }
        public List<ShortDTO> LineManagers { get; set; }
        public List<ShortDTO> HRs { get; set; }
        public ShortDTO CEO { get; set; }
        public ShortDTO ChiefAccountant { get; set; }
        public List<ShortDTO> BAs { get; set; }
        public List<ShortDTO> Admins { get; set; }

    }
}
