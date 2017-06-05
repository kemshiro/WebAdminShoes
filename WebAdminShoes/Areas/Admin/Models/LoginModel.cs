using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAdminShoes.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tên tài khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}