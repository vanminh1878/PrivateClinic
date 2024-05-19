using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThongTinThuocViewModel
    {
        public int MaThuoc { get; set; }
        public int MaLoaiThuoc { get; set; }
        public string TenThuoc { get; set; }
        public int MaDVT { get; set; }
        public int MaCachDung { get; set; }
        public float? DonGiaNhap { get; set; }
        public float? DonGiaBan { get; set; }

        public ICommand ExitCommand { get; set; }

        private ThongTinThuocView _view;

        // Hàm khởi tạo
        public ThongTinThuocViewModel(THUOC thuoc, ThongTinThuocView view)
        {
            MaThuoc = thuoc.MaThuoc;
            MaLoaiThuoc = thuoc.MaLoaiThuoc;
            TenThuoc = thuoc.TenThuoc;
            MaDVT = thuoc.MaDVT;
            MaCachDung = thuoc.MaCachDung;
            DonGiaNhap = (float?)thuoc.DonGiaNhap;
            DonGiaBan = (float?)thuoc.DonGiaBan;

            this._view = view;
        }

    }
}
