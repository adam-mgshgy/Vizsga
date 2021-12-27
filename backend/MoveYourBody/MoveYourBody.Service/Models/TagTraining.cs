﻿using System;
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
        public int Training_id { get; set; }
        [Required]
        public int Tag_id { get; set; }
    }
}
