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
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult AdminManager(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var listAdmin = dao.ListAllAdminPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(listAdmin);
        }

        public ActionResult UserManager(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var listUser = dao.ListAllUserPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(listUser);
        }

        public ActionResult Detail()
        {
            var dao = new UserDao();
            var userSession = Session[CommonConstants.USER_SESSION] as UserLogin;


            string username = userSession.username;

            TaiKhoan loginAcc = dao.GetAccByUsername(username);
            return View(loginAcc);
        }

        public ActionResult EditAdmin(int id)
        {
            var dao = new UserDao();
            var user = dao.GetAccById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditAdmin(TaiKhoan updateAcc)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.EditValidation(updateAcc.email, updateAcc.phone);

                switch (result)
                {
                    case 1:
                        bool kq = dao.Update(updateAcc);
                        if (kq == true)
                        {
                            return RedirectToAction("AdminManager", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật lỗi");
                        }

                        break;

                    case -1:
                        ModelState.AddModelError("", "Email không hợp lệ");
                        break;
                    case -2:
                        ModelState.AddModelError("", "Số điện thoại không hợp lệ");
                        break;

                    default:
                        ModelState.AddModelError("", "Đang xảy ra lỗi hệ thống, vui lòng đăng kí lại sau!");
                        break;
                }
            }
            return View("EditAdmin");
        }

        public ActionResult PasswordChange()
        {
            var userSession = Session[CommonConstants.USER_SESSION] as UserLogin;
            string username = userSession.username;
            var loginAcc = new PasswordChangeModel();
            loginAcc.username = username;

            return View(loginAcc);
        }

        [HttpPost]
        public ActionResult PasswordChange(PasswordChangeModel passwordChangeModel)
        {
            if (ModelState.IsValid)
            {
                var userSession = Session[CommonConstants.USER_SESSION] as UserLogin;
                string username = userSession.username;
                string oldPass = Encryptor.MD5Hash(passwordChangeModel.oldPassword);
                string newPass = Encryptor.MD5Hash(passwordChangeModel.newPassword);
                string confirm = Encryptor.MD5Hash(passwordChangeModel.confirmPassword);
                var dao = new UserDao();

                var result = dao.PasswordChangeValidation(passwordChangeModel.oldPassword, passwordChangeModel.newPassword, passwordChangeModel.confirmPassword);
                switch (result)
                {
                    case 1:
                        bool kq = dao.CheckUser(username, oldPass);
                        if (kq == true)
                        {
                            var entity = new TaiKhoan();
                            entity.username = username;
                            entity.password = newPass;
                            bool isPassChanged = dao.UpdatePassword(entity);
                            if (isPassChanged)
                            {
                                Session.Remove(CommonConstants.USER_SESSION);
                                return RedirectToAction("Index", "Login");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Cập nhật mật khẩu lỗi");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                        }

                        break;

                    case 0:
                        ModelState.AddModelError("", "Phải điền đầy đủ các trường");
                        break;

                    case -1:
                        ModelState.AddModelError("", "Mật khẩu mới không hợp lệ");
                        break;
                    case -2:
                        ModelState.AddModelError("", "Mật khẩu và xác nhận không giống nhau");
                        break;

                    default:
                        ModelState.AddModelError("", "Đang xảy ra lỗi hệ thống, vui lòng đăng kí lại sau!");
                        break;
                }
            }
            return View("PasswordChange");
        }

        [HttpDelete]
        public ActionResult DeleteAdmin(int id)
        {
            new UserDao().DeleteAdmin(id);

            return RedirectToAction("AdminManager");
        }

        public ActionResult StatusUpdate(int id)
        {
            var dao = new UserDao();
            var user = dao.GetAccById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult StatusUpdate(TaiKhoan updateAccount)
        {
            var dao = new UserDao();
            long id = updateAccount.id;

            TaiKhoan user = new TaiKhoan();
            user.id = id;
            user.status = updateAccount.status;

            if (ModelState.IsValid)
            {
                bool result = dao.StatusUpdate(user);
                if (result)
                {
                    RedirectToAction("UserManager", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa không thành công");
                }
            }

            return View("StatusUpdate");
        }

    }

}