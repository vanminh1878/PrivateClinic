using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class DoiMatKhauViewModel : BaseViewModel
    {
        #region Các Property và Command
        public ICommand ChangePasswordCommand { get; set; }

        private NGUOIDUNG user;
        public NGUOIDUNG User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        #endregion

        private DoiMatKhauView _view;

        //Hàm khởi tạo
        public DoiMatKhauViewModel(DoiMatKhauView view) 
        {
            ChangePasswordCommand = new RelayCommand<DoiMatKhauView>((p) => true, (p) => changePassword(p));
            this._view = view;
        }


        //Hàm thay đổi pass
        private void changePassword(object obj)
        {
            //Lấy tên đăng nhập hiện tại của user
            string tendangnhap = Const.TenDangNhap;
            //Lấy đối tượng đang dùng app
            User = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            if (_view.txtMKmoi.Password.ToString() == "" || _view.txtNhapLai.Password.ToString() == " " || _view.txtMKcu.Password.ToString() == " ")
            {
                MessageBox.Show("Chưa nhập đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (_view.txtMKcu.Password.ToString() != User.MatKhau) 
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(_view.txtMKmoi.Password.ToString().Contains(" ")) 
            {
                MessageBox.Show("Mật khẩu không được chứa khoảng trắng","Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else if(_view.txtMKmoi.Password.ToString() != _view.txtNhapLai.Password.ToString())
            {
                MessageBox.Show("Mật khẩu mới không khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                User.MatKhau = _view.txtMKmoi.Password.ToString();
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                _view.txtMKcu.Password = "";
                _view.txtNhapLai.Password = "";
                _view.txtMKmoi.Password = "";
            }
        }
    }
}
