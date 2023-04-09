namespace GB_Corporation.DTOs
{
    public class HiringCreateDTO : BaseDTO
    {
        public int ApplicantId { get; set; }
        public DateTime Date { get; set; }
        public int PositionId { get; set; }
        public List<int> Interviewers { get; set; }
        public int? ForeignLanguageTestId { get; set; }
        public int? LogicTestId { get; set; }
        public int? ProgrammingTestId { get; set; }
    }
}
