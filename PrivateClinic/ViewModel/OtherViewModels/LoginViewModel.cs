using PrivateClinic.Model;
using PrivateClinic.View.OtherViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace PrivateClinic.ViewModel.OtherViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public static bool IsLogin { get; set; }
        private string _Username;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password;
        private string _errorMessage;

        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LoadLoginPageCM { get; set; }
        public ICommand CloseLogin { get; set; }
        public ICommand MinimizeLogin { get; set; }
        public ICommand MoveLogin { get; set; }
        public Button LoginButton { get; set; }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            Username = "";
            CloseLogin = new RelayCommand<LoginView>((p) => true, (p) => Close());
            MinimizeLogin = new RelayCommand<LoginView>((p) => true, (p) => Minimize(p));
            MoveLogin = new RelayCommand<LoginView>((p) => true, (p) => Move(p));

            PasswordChangedCommand = new RelayCommand<PasswordBox>((FloatingPasswordBox) => { return true; }, (FloatingPasswordBox) => { Password = FloatingPasswordBox.Password; });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                try
                {
                    //string PassEncode = MD5Hash(Base64Encode(Password));
                    string PassEncode = Password;

                    //var accCount = DataProvider.Ins.DB.NGUOIDUNGs
                    //    .Where(x => x.TenDangNhap == Username && x.MatKhau == PassEncode && x.TenDangNhap.Equals(Username, StringComparison.Ordinal))
                    //    .Count();
                    var accCount = DataProvider.Ins.DB.NGUOIDUNG
                                                .Where(x => x.TenDangNhap.Trim() == Username.Trim() && x.MatKhau.Trim().Equals(PassEncode))
                                                .Count();
                    if (accCount > 0)
                    {
                        IsLogin = true;
                        Const.TenDangNhap = Username;
                        Window oldWindow = App.Current.MainWindow;
                        MainView mainView = new MainView();
                        App.Current.MainWindow = oldWindow;
                        oldWindow.Close();
                        mainView.Show();
                        Username = "";
                    }
                    else
                    {
                        ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    }
                }
                catch
                {
                    ErrorMessage = "Lỗi kết nối đến cơ sở dữ liệu!";
                }
            });

        }
        public void Close()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void Minimize(LoginView p)
        {
            p.WindowState = WindowState.Minimized;
        }
        public void Move(LoginView p)
        {
            //p.DragMove();
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        public static string MD5Hash(string value)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(Encoding.UTF8.GetBytes(value));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string Base64Encode(string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(plainTextBytes);
        }
    
    }
}
