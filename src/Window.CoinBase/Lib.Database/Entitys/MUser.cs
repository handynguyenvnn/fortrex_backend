namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MUser")]
    public partial class MUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? PasswordFormatId { get; set; }

        [StringLength(10)]
        public string PasswordSaft { get; set; }

        [StringLength(128)]
        public string LastIpAddress { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastActiveDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public bool IsLock { get; set; }

        public DateTime? LastLockDate { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int? ReferralId { get; set; }

        [StringLength(128)]
        public string WalletCoin { get; set; }

        [StringLength(128)]
        public string WalletETH { get; set; }

        [StringLength(128)]
        public string FA2Code { get; set; }

        [StringLength(150)]
        public string FA3Code { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public DateTime? UpdateOn { get; set; }

        public DateTime CreateOn { get; set; }

        [StringLength(50)]
        public string CountryId { get; set; }

        public int? TotalLoginFaild { get; set; }

        [StringLength(50)]
        public string CityId { get; set; }

        [StringLength(20)]
        public string Node { get; set; }

        [StringLength(128)]
        public string WalletXRP { get; set; }

        [StringLength(128)]
        public string WalletBCH { get; set; }

        [StringLength(128)]
        public string WalletBNCT { get; set; }
    }
}
