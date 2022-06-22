namespace GB_Corporation.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public int StatusId { get; set; }
        public SuperDictionary Status { get; set; }
    }
}
