﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class TrainingImages
    {
        [Key]
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public int TrainingId { get; set; }

    }
}