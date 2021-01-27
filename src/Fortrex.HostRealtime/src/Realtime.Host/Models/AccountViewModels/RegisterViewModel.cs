using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Host.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Roles")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Mã Cửa Hàng")]
        public string MaCuaHang { get; set; }
        [Required]
        [Display(Name = "Tên Cửa Hàng")]
        public string TenCuaHang { get; set; }
        [Required]
        [Display(Name = "Mã Vùng Miền")]
        public string MaVungMien { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class DangKyCuaHangViewModel
    {
       
        [Required]
        [Display(Name = "Mã Cửa Hàng")]
        public string MaCuaHang { get; set; }
        [Required]
        [Display(Name = "Tên Cửa Hàng")]
        public string TenCuaHang { get; set; }
        [Required]
        [Display(Name = "Mã Vùng Miền")]
        public string MaVungMien { get; set; }
        [Required]
        [Display(Name = "CpuId")]
        public string CpuId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
