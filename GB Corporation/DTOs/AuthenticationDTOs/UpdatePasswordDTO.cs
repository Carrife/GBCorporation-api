﻿namespace GB_Corporation.DTOs
{
    public class UpdatePasswordDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
