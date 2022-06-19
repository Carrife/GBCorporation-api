namespace GB_Corporation.DTOs.AuthenticationDTOs
{
    public class UpdatePasswordDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
