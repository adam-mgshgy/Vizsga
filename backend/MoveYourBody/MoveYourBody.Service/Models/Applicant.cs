using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int Training_session_id { get; set; }
        [Required]
        public int User_id { get; set; }
    }
}
