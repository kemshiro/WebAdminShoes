namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên tài khoản")]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ Tên")]
        public string name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ Email")]
        public string email { get; set; }

        [StringLength(30)]
        [Display(Name = "Số điện thoại")]
        public string phone { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? createdDate { get; set; }

        public bool? status { get; set; }

        public bool? isAdmin { get; set; }
    }
}
