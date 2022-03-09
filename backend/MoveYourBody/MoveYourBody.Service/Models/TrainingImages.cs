using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class TrainingImages
    {
        [Key]
        public int Id { get; set; }
        public int Image_id { get; set; }
        public int Training_id { get; set; }        

    }
}
