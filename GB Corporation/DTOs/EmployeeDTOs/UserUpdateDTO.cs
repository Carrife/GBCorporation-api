namespace GB_Corporation.DTOs
{
    public class UserUpdateDTO : BaseDTO
    {
        public int Role { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}
