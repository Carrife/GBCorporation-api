namespace GB_Corporation.DTOs
{
    public class ApplicantDTO : BaseDTO
    {
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public ShortDTO Status { get; set; }
    }
}
