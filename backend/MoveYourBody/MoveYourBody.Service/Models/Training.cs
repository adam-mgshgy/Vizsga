using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Training
    {
        [Required, Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public User Trainer { get; set; }
        [Required]
        public int Min_member { get; set; }
        [Required]
        public int Max_member { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        public string Contact_phone { get; set; }

    }
}
