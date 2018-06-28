using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eApp.Web.Admin.Models
{
    public class nLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        public string Region { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        public string CurrentPassword { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Mobile { get; set; }

        public string isSms { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Region { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }

        public string Password { get; set; }

    }

    public class UpdateInfoViewModel
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

    }

    public class nResetPasswordViewModel
    {
        public string cPassword { get; set; }
        public string nPassword { get; set; }

    }
}
