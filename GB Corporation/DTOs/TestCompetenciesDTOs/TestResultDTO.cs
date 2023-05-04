namespace GB_Corporation.DTOs
{
    public class TestResultDTO : BaseDTO
    {
        public string Employee { get; set; }
        public string Test { get; set; }
        public string? Result { get; set; }
        public string Date { get; set; }
    }
}
