namespace GB_Corporation.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public string Login { get; set; }
        public ShortDTO Role { get; set; }
        public string Email { get; set; }
    }
}
