using System;
using System.Collections.Generic;
using System.Text;

namespace MoveYourBody.WebAPI.Tests
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Full_name { get; set; }
        public string PasswordHash { get; set; } = "";
        private string password;
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
        public string Phone_number { get; set; }
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
