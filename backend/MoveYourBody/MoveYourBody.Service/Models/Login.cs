using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Login
    {
        [Required, StringLength(320)]
        public string Email { get; set; }
       
        [Required, StringLength(255)]
        public string Password { get; set; }
    }
}
