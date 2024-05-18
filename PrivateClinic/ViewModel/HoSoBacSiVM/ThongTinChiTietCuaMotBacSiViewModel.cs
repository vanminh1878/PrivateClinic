using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinChiTietCuaMotBacSiViewModel
    {
        #region các property
        public string MaBS {  get; set; }
        public string formatMaBS { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime?NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }

        public ICommand ExitCommand { get; set; }

        public ThongTinChiTietCuaMotBacSIView _view;
        #endregion

        //Hàm khởi tạo
        public ThongTinChiTietCuaMotBacSiViewModel(BACSI bs,ThongTinChiTietCuaMotBacSIView view) 
        {
            formatMaBS = bs.formatMaBS;
            HoTen = bs.HoTen;
            GioiTinh = bs.GioiTinh;
            SDT = bs.SDT;
            Email = bs.Email;
            NgaySinh = bs.NgaySinh;
            NgayVL = bs.NgayVaoLam;
            DiaChi = bs.DiaChi;
            BangCap = bs.BangCap;
            ExitCommand = new ViewModelCommand(Exit);
            this._view = view;

        }

        //Hàm hỗ trợ nút thoát
        private void Exit(object obj)
        {
            _view.Close();
        }
        
    }
}
