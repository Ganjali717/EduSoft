using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSoft.Model.DTO.Account
{
    public class LoginDto
    {
        public string? ReturnUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
