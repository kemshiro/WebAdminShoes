using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoes.Areas.Admin.Models
{
    public class PasswordChangeModel
    {
        [Display(Name = "Tên tài khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mời nhập password cũ")]
        [Display(Name = "Mật khẩu cũ")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "Mời nhập password mới")]
        [Display(Name = "Mật khẩu mới")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Mời xác nhận lại password mới")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string confirmPassword { get; set; }
    }
}