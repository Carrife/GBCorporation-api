namespace GB_Corporation.DTOs.AuthenticationDTOs
{
    public class RegisterDTO
    {
        public string NameRu { get; set; }
        public string SurnameRu { get; set; }
        public string PatronymicRu { get; set; }
        public string NameEn { get; set; }
        public string SurnameEn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? LanguageId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
