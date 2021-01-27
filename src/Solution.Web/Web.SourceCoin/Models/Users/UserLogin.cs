using System.ComponentModel.DataAnnotations;

namespace Web.SourceCoin.Models.Users
{
    public class UserLogin : Alert
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public string FACode { get; set; }
        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TwoFACode { get; set; }
    }

    public class LoginResponse
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
    public class ActiveEmailRegitserModel
    {
        public string token { get; set; }
    }
    public class UpdatePasswordModel
    {
        public string PassOld { get; set; }
        public string PassNew { get; set; }
        public string PassNewRe { get; set; }

    }
    public class ResetPasswordModel
    {
        public string PassNew { get; set; }
        public string PassNewRe { get; set; }
        public string Token { get; set; }
    }
}