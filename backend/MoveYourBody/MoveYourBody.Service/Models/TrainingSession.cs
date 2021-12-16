using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class TrainingSession
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        public int Training_id { get; set; }
        [Required]
        public int Location_id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Minutes { get; set; }

    }
}
