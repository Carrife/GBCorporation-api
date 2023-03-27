namespace GB_Corporation.DTOs
{
    public class ProgrammingTestDTO : BaseDTO
    {
        public string ProgrammingLanguage { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantId { get; set; }
    }
}
