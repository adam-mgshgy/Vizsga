using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class Category
    {
        [Required, Key]
        public string Name { get; set; }
    }
}
