using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdminShoes.Areas.Admin.Models;
using WebAdminShoes.Common;

namespace WebAdminShoes.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Register(registerModel.username,
                    registerModel.password,
                    registerModel.confirmPass,
                    registerModel.name,
                    registerModel.email,
                    registerModel.phone);

                switch (result)
                {
                    case 1:
                        var username = registerModel.username;
                        var password = Encryptor.MD5Hash(registerModel.password);
                        var name = registerModel.name;
                        var email = registerModel.email;
                        var phone = registerModel.phone;

                        long id = dao.Insert(username,password,name,email,phone);
                        if (id > 0)
                        {
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Không thể đăng kí bây giờ");
                        }

                        break;

                    case 0:
                        ModelState.AddModelError("", "Tài khoản đã tồn tại mời đăng nhập, hoặc đăng kí một tài khoản khác!");
                        return RedirectToAction("Index", "Login");

                    case -1:
                        ModelState.AddModelError("", "username phải từ 4 -> 10 kí tự, không được có kí tự đặc biệt");
                        break;

                    case -2:
                        ModelState.AddModelError("", "Mật khẩu phải từ 4-> 8 kí tự, bắt buộc có 1 chữ viết hoa, 1 số và không có kí tự đặc biệt.");
                        break;

                    case -3:
                        ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không giống nhau");
                        break;

                    case -4:
                        ModelState.AddModelError("", "Email không hợp lệ.");
                        break;

                    case -5:
                        ModelState.AddModelError("", "Số điện thoại không hợp lệ");
                        break;

                    default:
                        ModelState.AddModelError("", "Đang xảy ra lỗi hệ thống, vui lòng đăng kí lại sau!");
                        break;
                }
            }
            return View("Index");
        }
    }
}