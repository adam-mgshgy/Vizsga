using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace MoveYourBody.Service.Models
{
    public class User
    {
        [Required, Key]
        public int Id { get; set; }
        [Required, StringLength(320)]
        public string Email { get; set; }
        [Required, StringLength(255)]
        public string Full_name { get; set; }
        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = "";
        private string password;
        [NotMapped]
        public string Password
        { 
            get
            { 
                return password;
            }
            set 
            { 
                password = value;
                this.PasswordHash = HashPassword(password);
            } 
        }
        [Required, StringLength(12)]
        public string Phone_number { get; set; }
        [Required]
        public int Location_id { get; set; }
        public string Role { get; set; }
        public int ImageId { get; set; }

        public bool CheckPassword(string pwd)
        {
            return HashPassword(pwd) == this.PasswordHash;
        }
        private string HashPassword(string pwd)
        {
            HashAlgorithm sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            return Convert.ToBase64String(hash);
        }
    }
}
