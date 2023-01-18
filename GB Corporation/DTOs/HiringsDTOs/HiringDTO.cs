namespace GB_Corporation.DTOs
{
    public class HiringDTO : BaseDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? LanguageId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
