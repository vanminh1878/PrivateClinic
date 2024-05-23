using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinChiTietCuaMotBacSiViewModel
    {
        #region các property
        public int MaBS {  get; set; }
        public string formatMaBS { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime?NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }
        public BitmapImage ImageSource { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ThongTinChiTietCuaMotBacSIView _view;
        DanhSachBacSiViewModel _viewModel {  get; set; }
        #endregion

        //Hàm khởi tạo
        public ThongTinChiTietCuaMotBacSiViewModel(BACSI bs,ThongTinChiTietCuaMotBacSIView view) 
        {
            HoTen = bs.HoTen;
            MaBS = bs.MaBS;
            formatMaBS = bs.formatMaBS;
            GioiTinh = bs.GioiTinh;
            SDT = bs.SDT;
            Email = bs.Email;
            NgaySinh = bs.NgaySinh;
            NgayVL = bs.NgayVaoLam;
            DiaChi = bs.DiaChi;
            BangCap = bs.BangCap;
            if (bs.Image == null)
            {
                Uri resourceUri = new Uri("pack://application:,,,/ResourceXAML/image/img_default.png", UriKind.Absolute);
                StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(resourceUri);
                byte[] imageBytes;
                using (Stream imageStream = streamInfo.Stream)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageStream.CopyTo(ms);
                        imageBytes = ms.ToArray();
                    }
                }
                ImageSource = ImageViewModel.ByteArrayToBitmapImage(imageBytes);
            }
            else
            {
                BitmapImage image = ImageViewModel.ByteArrayToBitmapImage(bs.Image);
                ImageSource = image;
            }
            ExitCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => true, (p) => Close(p));
            EditCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _EditCommand(p));
            SaveCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
            DeleteCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            this._view = view;

        }

        //Hàm hỗ trợ nút thoát
        void Close(ThongTinChiTietCuaMotBacSIView p)
        {
            p.Close();
        }

        //Cho phép sửa
        void _EditCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {

            parameter.Ngaysinhtxb.IsEnabled = true;
            parameter.NgayVaoLamtxb.IsEnabled = true;
            parameter.HoTentxb.IsEnabled = true;
            parameter.GioiTinhtxb.IsEnabled = true;
            parameter.Emailtxb.IsEnabled = true;
            parameter.DiaChitxb.IsEnabled = true;
            parameter.SDTtxb.IsEnabled = true;
            parameter.Emailtxb.IsEnabled = true;
            parameter.BangCaptxb.IsEnabled = true;
            parameter.btnSave.Visibility = System.Windows.Visibility.Visible;
            parameter.btnSave.IsEnabled = true;

        }

        //Lưu thông tin bác sĩ sau khi sửa
        void _SaveCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            BACSI t = DataProvider.Ins.DB.BACSIs.SingleOrDefault(h => h.MaBS == MaBS);

            MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu lại thông tin bác sĩ không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (parameter.SDTtxb.Text == "" || parameter.Emailtxb.Text == "" || parameter.BangCaptxb.Text == "" ||
                       parameter.DiaChitxb.Text == "" || parameter.HoTentxb.Text == "" || parameter.Ngaysinhtxb.SelectedDate == null || parameter.GioiTinhtxb.SelectedValue==null
                       || parameter.NgayVaoLamtxb.SelectedDate == null)
                {
                    MessageBox.Show("Nhập thiếu thông tin", "Thông báo", MessageBoxButton.OK);
                }
                else if (!parameter.Emailtxb.Text.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ","Thông báo", MessageBoxButton.OK);
                }
                else if (!parameter.SDTtxb.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại chỉ chứa số", "Thông báo", MessageBoxButton.OK);
                }
                else if (parameter.Ngaysinhtxb.SelectedDate.Value > DateTime.UtcNow)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ!", "Thông báo", MessageBoxButton.OK);
                }
                else if (parameter.NgayVaoLamtxb.SelectedDate.Value <  parameter.Ngaysinhtxb.SelectedDate)
                {
                    MessageBox.Show("Ngày vào làm phải lớn hơn ngày sinh!", "Thông báo", MessageBoxButton.OK);
                }
                else
                {
                    t.HoTen = parameter.HoTentxb.Text;
                    t.SDT = parameter.SDTtxb.Text;
                    t.Email = parameter.Emailtxb.Text;
                    t.BangCap = parameter.BangCaptxb.Text;
                    t.NgayVaoLam = parameter.NgayVaoLamtxb.SelectedDate.Value;
                    t.NgaySinh = parameter.Ngaysinhtxb.SelectedDate;
                    t.GioiTinh = parameter.GioiTinhtxb.SelectedValue.ToString();
                    t.DiaChi = parameter.DiaChitxb.Text;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đã lưu thông tin bác sĩ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    parameter.btnSave.Visibility = Visibility.Hidden;
                }
                
            }
        }

        //Xóa thông tin bác sĩ
        void _DeleteCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            BACSI selectedItem = DataProvider.Ins.DB.BACSIs.SingleOrDefault(h => h.MaBS == MaBS);
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bác sĩ này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(r == MessageBoxResult.Yes)
            {
                if (selectedItem != null && selectedItem.MaBS != 1)
                {
                    //Xóa tài khoàn trước
                    var taikhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.NGUOIDUNGs.RemoveRange(taikhoan);
                    //Cập nhật mã bác sĩ trong các phiếu khám bệnh bằng NULL
                    var phieukhambenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    foreach (var h in phieukhambenh)
                        h.MaBS = null;
                    //Xóa bác sĩ
                    DataProvider.Ins.DB.BACSIs.Remove(selectedItem);

                    //Lưu xuống CSDL
                    DataProvider.Ins.DB.SaveChanges();
                    //Cập nhật listview


                    MessageBox.Show("Xóa thành công", "Thông báo");
                    parameter.Close();
                }
                else
                {
                    MessageBox.Show("Không thể xóa Admin", "Báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            
        }

        

    }
}
