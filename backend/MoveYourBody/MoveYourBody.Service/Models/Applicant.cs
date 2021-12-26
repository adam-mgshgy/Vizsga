using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Applicant
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        public TrainingSession Training_session { get; set; }
        [Required]
        public User User { get; set; }
    }
}
