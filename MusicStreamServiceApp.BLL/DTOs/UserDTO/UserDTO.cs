using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.BLL.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhotoPath { get; set; }
    }
}
