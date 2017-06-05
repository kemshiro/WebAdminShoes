using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        private WebAdminShoesDbContext db = null;
        private DataValidation dataValidation;

        public UserDao()
        {
            db = new WebAdminShoesDbContext();
            dataValidation = new DataValidation();
        }

        public bool CheckUser(string username, string pass)
        {
            var result = db.TaiKhoans.Count(x => x.username == username && x.password == pass);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TaiKhoan GetAccByUsername(string username)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.username == username);
        }

        public TaiKhoan GetAccById(long id)
        {
            return db.TaiKhoans.Find(id);
        }

        public int EditValidation(string email, string phone)
        {
            if (!dataValidation.EmailValidation(email))
            {
                return -1;
            }
            else
            {
                if (!dataValidation.PhoneNumberValidation(phone))
                {
                    return -2;
                }
                else
                {
                    return 1;
                }
            }

        }

        public int PasswordChangeValidation(string oldPassword, string newPassword, string confirmPass)
        {
            if (string.IsNullOrEmpty(oldPassword))
            {
                return 0;
            }
            else
            {
                if (!dataValidation.PasswordValidation(newPassword))
                {
                    return -1;
                }
                else
                {
                    if (newPassword.CompareTo(confirmPass) != 0)
                    {
                        return -2;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }

        public int Register(string username, string password, string confirmPass, string name, string email, string phone)
        {
            //tìm kiếm tài khoản có username đã tồn tại chưa
            var result = db.TaiKhoans.SingleOrDefault(x => x.username == username);

            if (result != null) // nếu tài khoản đã tồn tại
            {
                return 0;
            }
            else // nếu chưa tồn tại
            {
                if (!dataValidation.UsernameValidation(username)) // username không hợp lệ
                {
                    return -1;
                }
                else
                {
                    if (!dataValidation.PasswordValidation(password)) //password không hợp lệ
                    {
                        return -2;
                    }
                    else
                    {
                        if (confirmPass.CompareTo(password) != 0) //xác nhận và pass không giống nhau
                        {
                            return -3;
                        }
                        else
                        {
                            if (!dataValidation.EmailValidation(email)) // email không hợp lệ
                            {
                                return -4;
                            }
                            else
                            {
                                if (!dataValidation.PhoneNumberValidation(phone)) // phone không hợp lệ
                                {
                                    return -5;
                                }
                                else
                                {
                                    return 1;
                                }

                            }
                        }
                    }
                }
            }
        }

        public int Login(string username, string password)
        {
            //kiểm tra username có tồn tại không
            var result = db.TaiKhoans.SingleOrDefault(x => x.username == username && x.isAdmin == true);
            if (result == null) // nếu không
            {
                return 0;
            }
            else// nếu có tồn tại tài khoản
            {
                //tài khoản chưa kích hoạt
                if (result.status == false)
                {
                    return -1;
                }
                else
                {
                    //đúng cả id và pass
                    if (result.password == password)
                    {
                        return 1;
                    }
                    //tài khoản sai mật khẩu
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        public long Insert(string username, string password, string name, string email, string phone)
        {
            try
            {
                var taiKhoan = new TaiKhoan();
                taiKhoan.username = username;
                taiKhoan.password = password;
                taiKhoan.name = name;
                taiKhoan.email = email;
                taiKhoan.phone = phone;
                taiKhoan.createdDate = DateTime.Now;
                taiKhoan.isAdmin = true;
                taiKhoan.status = true;

                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return taiKhoan.id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public bool Update(TaiKhoan entity)
        {
            try
            {
                var user = db.TaiKhoans.SingleOrDefault(x => x.id == entity.id);
                user.name = entity.name;
                user.email = entity.email;
                user.phone = entity.phone;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool StatusUpdate(TaiKhoan entity)
        {

            try
            {
                var user = db.TaiKhoans.Find(entity.id);
                user.status = entity.status;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool UpdatePassword(TaiKhoan entity)
        {
            try
            {
                var user = db.TaiKhoans.SingleOrDefault(x => x.username == entity.username);
                user.password = entity.password;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAdmin(int id)
        {
            try
            {
                var user = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public IEnumerable<TaiKhoan> ListAllAdminPaging(string searchString, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans.Where(x => x.isAdmin == true);
            if (!string.IsNullOrEmpty(searchString)) // chuoi tim kiem bang rong
            {
                model = model.Where(x => x.username.Contains(searchString) || x.name.Contains(searchString)).OrderByDescending(x => x.id);
            }

            return model.OrderByDescending(x => x.id).ToPagedList(page, pageSize);
        }

        public IEnumerable<TaiKhoan> ListAllUserPaging(string searchString, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans.Where(x => x.isAdmin == false);
            if (!string.IsNullOrEmpty(searchString)) // chuoi tim kiem bang rong
            {
                model = model.Where(x => x.username.Contains(searchString) || x.name.Contains(searchString)).OrderByDescending(x => x.id);
            }

            return model.OrderByDescending(x => x.id).ToPagedList(page, pageSize);
        }

        public int countAllActiveUser()
        {
            return db.TaiKhoans.Count(x => x.status == true && x.isAdmin == false);
        }
    }
}
