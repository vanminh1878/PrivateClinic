﻿using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThemBacSiViewModel :BaseViewModel
    {
        #region Các Command và Property
        // Command thoát khỏi màn hình thêm bác sĩ
        public ICommand quitCommand { get; set; }

        //Command thêm bác sĩ
        public ICommand AddDoctorCommand { get; set; }
        //Notify
        private string notify;
        public string Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged(nameof(Notify));
            }
        }

        //Họ Tên
        private string hoten;
        public string HoTen
        {
            get => hoten;
            set
            {
                hoten = value;
                ValidateFullName();
            }
        }

        private string hotenError;
        public string HoTenError
        {
            get => hotenError;
            set
            {
                hotenError = value;
                OnPropertyChanged(nameof(HoTenError));
            }
        }

        //Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
            }
        }

        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }

        //SĐT
        private string sdt;
        public string SDT
        {
            get => sdt;
            set
            {
                sdt = value;
                ValidateSDT();
            }
        }

        private string sdtError;
        public string SDTError
        {
            get => sdtError;
            set
            {
                sdtError = value;
                OnPropertyChanged(nameof(SDTError));
            }
        }

        //DiaChi
        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                diachi = value;
                ValidateDiaChi();
            }
        }

        private string diachiError;
        public string DiaChiError
        {
            get => diachiError;
            set
            {
                diachiError = value;
                OnPropertyChanged(nameof(DiaChiError));
            }
        }

        //BangCap
        private string bangcap;
        public string BangCap
        {
            get => bangcap;
            set
            {
                bangcap = value;
                ValidateBangCap();
            }
        }

        private string bangcapError;
        public string BangCapError
        {
            get => bangcapError;
            set
            {
                bangcapError = value;
                OnPropertyChanged(nameof(BangCapError));
            }
        }

        //GioiTinh
        public string GioiTinh { get; set; }
        //NgaySinh
        private DateTime? ngaysinh;
        public DateTime? NgaySinh
        {
            get => ngaysinh;
            set
            {
                ngaysinh = value;
                ValidateBirth();
            }
        }
        private string ngaysinhError;
        public string NgaySinhError
        {
            get => ngaysinhError;
            set
            {
                ngaysinhError = value;
                OnPropertyChanged(nameof(NgaySinhError));
            }
        }
        //NgayVaoLam
        private DateTime? ngayvl;
        public DateTime? NgayVL
        {
            get => ngayvl;
            set
            {
                ngayvl = value;
                ValidateNgayVL();
            }
        }
        private string ngayvlError;
        public string NgayVLError
        {
            get => ngayvlError;
            set
            {
                ngayvlError = value;
                OnPropertyChanged(nameof(NgayVLError));
            }
        }

        private bool[] _canAccept = new bool[7];

        private ThemBacSiView view;
        #endregion

        //Hàm khởi tạo
        public ThemBacSiViewModel(ThemBacSiView wd) 
        {
            this.view = wd;
            quitCommand = new ViewModelCommand(quit);
            AddDoctorCommand = new ViewModelCommand(acceptAdd,canAcceptAdd);
            GioiTinh = "Nam";
            NgayVL = DateTime.UtcNow;
            NgaySinh = DateTime.UtcNow;
        }

        #region Các hàm
        private void quit(object obj)
        {
            view.Close();
        }
        private bool canAcceptAdd(object obj)
        {
            bool check = _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] 
                            && _canAccept[4] && _canAccept[5] && _canAccept[6];
            if (check)
            {
                return true;
            }
            return false;
            
        }
        private void acceptAdd(object obj)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm bác sĩ mới ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                BACSI bacsi = new BACSI();
                bacsi.HoTen = HoTen;
                bacsi.Email = Email;
                bacsi.GioiTinh = GioiTinh;
                bacsi.SDT = SDT;
                bacsi.NgaySinh = NgaySinh;
                bacsi.NgayVaoLam = (DateTime)NgayVL;
                bacsi.DiaChi = DiaChi;
                bacsi.BangCap = BangCap;
                DataProvider.Ins.DB.BACSI.Add(bacsi);
                DataProvider.Ins.DB.SaveChanges();
                //Thêm tài khoản
                NGUOIDUNG nguoi = new NGUOIDUNG();
                nguoi.MaBS = bacsi.MaBS;
                nguoi.MaNhom = "NHOM2";
                nguoi.TenDangNhap = TaoTenDangNhap(bacsi);
                nguoi.MatKhau = TaoMK();
                DataProvider.Ins.DB.NGUOIDUNG.Add(nguoi);
                DataProvider.Ins.DB.SaveChanges();
                //Gửi mail tài khoản cho bác sĩ
                Task.Run(async () =>
                {
                    string email = "privateclinicse104@gmail.com";
                    string pass = "uwui fldf dxif drqj";
                    MailMessage mailMessage = new MailMessage();
                    try
                    {
                        mailMessage.From = new MailAddress(email);
                        mailMessage.Subject = "Tài khoản đăng nhập App";
                        mailMessage.To.Add(new MailAddress(bacsi.Email));
                    }
                    catch
                    {

                    }
                    GuiTaiKhoan(nguoi.TenDangNhap,nguoi.MatKhau,mailMessage,email,pass);
                    await Task.Delay(2000);
                }
                );
                MessageBox.Show("Thêm bác sĩ mới thành công\n"+"Tài khoản đã được gửi qua mail.", "Thông báo");
                view.Close();
            }
        }

        //Hàm hỗ trợ tạo tên đăng nhập
        private string TaoTenDangNhap(BACSI bs)
        {
            if (bs.MaBS < 10)
            {
                return "user00" + bs.MaBS.ToString();
            }
            else if (bs.MaBS < 100)
            {
                return "user0" + bs.MaBS.ToString();
            }
            else
            {
                return "user" + bs.MaBS.ToString();
            }
        }

        //Hàm hỗ trợ tạo tên đăng nhập
        private string TaoMK()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] passwordChars = new char[6];

            for (int i = 0; i < 6; i++)
            {
                passwordChars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(passwordChars);
        }
        //Hàm tự động gửi mật khẩu qua email
        void GuiTaiKhoan(string tendangnhap, string matkhau, 
                        MailMessage mailMessage, string fromMail, string fromPassword)
        {
            string noidung =
                            "Tài khoản của bạn là: " + "<br>"
                            + "Tên đăng nhập: " + "<b>" + tendangnhap + "</b>" + "<br>"
                            + "Mật khẩu: " + "<b>" + matkhau + "</b>" + "<br>"
                            + "Thân mến!";

            mailMessage.Body = "<html><body>" + noidung + "</body></html>";
            mailMessage.IsBodyHtml = true;
            //Gửi mail
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi gửi email
                    MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Thông báo");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi khởi tạo SmtpClient
                MessageBox.Show("Lỗi khi khởi tạo SmtpClient: " + ex.Message, "Thông báo");
            }
        }
        //Các hàm báo lỗi
        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(HoTen))
            {
                HoTenError = "Họ và tên không được để trống!";
                _canAccept[0] = false;
            }
            else
            {
                HoTenError = "";
                _canAccept[0] = true;
            }
        }
        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email không được để trống!";
                _canAccept[1] = false;
            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email không hợp lệ!";
                _canAccept[1] = false;
            }
            else
            {
                EmailError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidateSDT()
        {
            if (string.IsNullOrWhiteSpace(SDT))
            {
                SDTError = "SĐT không được để trống!";
                _canAccept[2] = false;
            }
            else if (!SDT.All(char.IsDigit))
            {
                SDTError = "SĐT chỉ chứa số";
                _canAccept[2] = false;
            }
            else
            {
                SDTError = "";
                _canAccept[2] = true;
            }
        }
        private void ValidateDiaChi()
        {
            if (string.IsNullOrWhiteSpace(DiaChi))
            {
                DiaChiError = "Địa chỉ không được để trống!";
                _canAccept[3] = false;
            }
            else
            {
                DiaChiError = "";
                _canAccept[3] = true;
            }
        }
        private void ValidateBangCap()
        {
            if (string.IsNullOrWhiteSpace(BangCap))
            {
                BangCapError = "Bằng cấp không được để trống!";
                _canAccept[4] = false;
            }
            else
            {
                BangCapError = "";
                _canAccept[4] = true;
            }
        }
        private void ValidateBirth()
        {
            if (NgaySinh > DateTime.UtcNow)
            {
                NgaySinhError = "Ngày sinh không hợp lệ!";
                _canAccept[5] = false;
            }
            else
            {
                NgaySinhError = "";
                _canAccept[5] = true;
            }
        }
        private void ValidateNgayVL()
        {
            if (NgayVL < NgaySinh)
            {
                NgayVLError = "Ngày vào làm phải lớn hơn ngày sinh!";
                _canAccept[6] = false;
            }
            else
            {
                NgayVLError = "";
                _canAccept[6] = true;
            }
        }
        #endregion

    }
}
