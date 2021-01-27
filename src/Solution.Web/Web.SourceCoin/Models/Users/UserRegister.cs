using System.ComponentModel.DataAnnotations;

namespace Web.SourceCoin.Models.Users
{
    public class UserRegister : Alert
    {
        public string ReferralId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordComfirm { get; set; }
        public string ReferralName { get; set; }
        public string Phone { get; set; }
        public string PhoneNatural { get; set; }
        public string Node { get; set; }
    }

    public class RegisterModel
    {
        [MaxLength(20)]
        public string ReferralId { get; set; }
        [MaxLength(20)]
        public string ReferralCode { get; set; }
        [MaxLength(50)]
        public string Fullname { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string PasswordConfirm { get; set; }
        [MaxLength(20)]
        public string Country { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
    }
}