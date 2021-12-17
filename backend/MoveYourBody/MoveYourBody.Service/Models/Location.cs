using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Location
    {
        [Required, Key]
        public int Id { get; set; }
        
       
        [Required, StringLength(255)]
        public string County_name { get; set; }
        [Required, StringLength(50)]
        public string City_name { get; set; }

    }
}
