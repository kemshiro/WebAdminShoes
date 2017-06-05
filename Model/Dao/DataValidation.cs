using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Dao
{
    public class DataValidation
    {
        public bool UsernameValidation(string username)
        {
            bool kq = true;
            if (username.Length < 4 && username.Length > 10)
            {
                kq = false;
            }
            else
            {
                char[] stringArray = username.ToCharArray();
                for (int i = 0; i < stringArray.Length; i++)
                {
                    int result = (int)stringArray[i];
                    if (result < 48
                        || result > 57 && result < 65
                        || result > 90 && result < 97
                        || result > 122)
                    {
                        kq = false;
                    }
                }
            }
            return kq;
        }

        private bool PasswordCheck1(string password)
        {
            if (password.Length < 4 && password.Length > 8) //kiểm tra số lượng kí tự
            {
                return false;
            }
            else
            {
                char[] charArr = password.ToCharArray(); // đổi chuỗi thành mảng char
                int i = 0;  //Lần 1 kiểm tra các kí tự trong mảng xem có kí tự đặc biệt không
                while (i < charArr.Length)
                {
                    int ascii = (int)charArr[i];
                    if (ascii < 48
                        || ascii > 57 && ascii < 65
                        || ascii > 90 && ascii < 97
                        || ascii > 122) // nếu có thì kết thúc luôn kiểm tra luông => chuỗi không được validate
                    {
                        return false;
                    }
                    i++;
                }
                i = 0; // cho i về 0 để kiểm tra từ đầu đến cuối lần 2
                while (i < charArr.Length)
                {
                    int ascii = (int)charArr[i];
                    if (ascii >= 48 && ascii <= 57) // nếu tồn tại 1 kí tự từ 0 - > 9
                    {
                        for (int j = i + 1; j < charArr.Length - i; j++) // kiểm tra các kí tự còn lại xem có các kí tự A->Z không
                        {
                            int ascii1 = (int)charArr[j];
                            if (ascii1 >= 65 && ascii1 <= 90)
                            {
                                return true; //nếu có cả 2 loại thì chuỗi được validate
                            }
                            j++;
                        }
                    }
                    i++;
                }

            }
            return false; //nếu không có 1 trong 2 loại thì không được validate
        }

        private bool PasswordCheck2(string password)
        {
            if (password.Length < 4 && password.Length > 8)//kiểm tra số lượng kí tự
            {
                return false;
            }
            else
            {
                char[] charArr = password.ToCharArray();
                int i = 0; //Lần 1 kiểm tra các kí tự trong mảng xem có kí tự đặc biệt không
                while (i < charArr.Length)
                {
                    int ascii = (int)charArr[i];
                    if (ascii < 48
                        || ascii > 57 && ascii < 65
                        || ascii > 90 && ascii < 97
                        || ascii > 122)
                    {
                        return false; // nếu có thì kết thúc luôn kiểm tra luông => chuỗi không được validate
                    }
                    i++;
                }
                i = 0;  // cho i về 0 để kiểm tra từ đầu đến cuối lần 2
                while (i < charArr.Length)
                {
                    int ascii = (int)charArr[i];
                    if (ascii >= 65 && ascii <= 90) // nếu tồn tại 1 kí tự từ A - > Z
                    {
                        for (int j = i + 1; j < charArr.Length - i; j++) // kiểm tra các kí tự còn lại xem có các kí tự 0->9 không
                        {
                            int ascii1 = (int)charArr[j];
                            if (ascii1 >= 48 && ascii1 <= 57)
                            {
                                return true; //nếu có cả 2 loại thì chuỗi được validate
                            }
                            j++;
                        }
                    }
                    i++;
                }

            }
            return false; //nếu không có 1 trong 2 loại thì không được validate
        }

        public bool PasswordValidation(string password)
        {
            if (PasswordCheck1(password) || PasswordCheck2(password))
            {
                return true;
            }
            return false;
        }

        public bool EmailValidation(string email)
        {
            if (!email.Contains("@") || !email.Contains(".com"))
            {
                return false;
            }
            return true;
        }

        public bool PhoneNumberValidation(string phoneNumber)
        {
            char[] charArr = phoneNumber.ToCharArray();
            if (charArr.Length > 11 || charArr.Length < 10 || charArr[0] != '0')
            {
                return false;
            }
            else
            {
                for (int i = 0; i < charArr.Length; i++)
                {
                    int ascii = (int)charArr[i];
                    if (ascii < 48 || ascii > 57)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
    }
}