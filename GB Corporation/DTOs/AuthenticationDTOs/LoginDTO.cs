using System.Text.Json.Serialization;

namespace GB_Corporation.DTOs.AuthenticationDTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
