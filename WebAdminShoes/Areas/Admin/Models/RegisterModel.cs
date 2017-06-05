using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoes.Areas.Admin.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Mời nhập tên tài khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string password { get; set; }

        [Required(ErrorMessage = "Mời xác nhận mật khẩu")]
        public string confirmPass { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string phone { get; set; }
    }
}