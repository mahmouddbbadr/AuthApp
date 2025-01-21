using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Application.DTOs
{
    public class AppUserRegisterDTO
    {
        [Required]
        [RegularExpression(@"^[\w\-\.]+@([\w\-]+\.)+[\w\-]{2,4}$", ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters, include at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^01[0-25]\d{8}$", ErrorMessage = "please enter a valid phone number")]
        public string PhoneNumber { get; set; }

    }
}
