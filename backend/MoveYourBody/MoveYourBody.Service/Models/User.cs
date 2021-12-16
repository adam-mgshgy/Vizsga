using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class User
    {
        [Required, Key]
        public int Id { get; set; }
        [Required, StringLength(320)]
        public string Email { get; set; }
        [Required, StringLength(255)]
        public string Password { get; set; }
        [Required, StringLength(12)]
        public string Phone_number { get; set; }
        [Required]
        public bool Trainer { get; set; }
        [Required]
        public int Location_id { get; set; }
    }
}
