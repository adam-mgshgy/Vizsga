using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Training
    {
        [Required, Key]
        public int Id { get; set; } //id name cat trainer minm maxm desc phone
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Category_id { get; set; }
        [Required]
        public int Trainer_id { get; set; }
        [Required]
        public int Min_member { get; set; }
        [Required]
        public int Max_member { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        public string Contact_phone { get; set; }

    }
}
