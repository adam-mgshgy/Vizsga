using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class TagTraining
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Training Training { get; set; }
        [Required]
        public Tag Tag { get; set; }
    }
}
