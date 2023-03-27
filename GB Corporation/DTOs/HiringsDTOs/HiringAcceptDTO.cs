namespace GB_Corporation.DTOs
{
    public class HiringAcceptDTO : BaseDTO
    {
        public int ApplicantId { get; set; }
        public int? LanguageId { get; set; }
        public int DepartmentId { get; set; }
    }
}
