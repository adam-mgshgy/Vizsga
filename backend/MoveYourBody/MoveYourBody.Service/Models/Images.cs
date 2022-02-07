using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
    }
}
