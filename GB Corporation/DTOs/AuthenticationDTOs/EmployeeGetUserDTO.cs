namespace GB_Corporation.DTOs
{
    public class EmployeeGetUserDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string Login { get; set; }
    }
}
