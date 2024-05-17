using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinChiTietCuaMotBacSiViewModel : BaseViewModel
    {
        #region các property
        private ObservableCollection<BACSI> _listBS;
        public ObservableCollection<BACSI> listBS { get => _listBS; set { _listBS = value; OnPropertyChanged(); } }
        public string MaBS {  get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime?NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }

        public ICommand ExitCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ThongTinChiTietCuaMotBacSIView _view;
        #endregion
        public ThongTinChiTietCuaMotBacSiViewModel() 
        {
            listBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            ExitCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => true, (p) => Close(p));
            LoadCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => true, (p) => _LoadCommand(p));
            EditCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _EditCommand(p));
            SaveCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
            DeleteCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
        }
        void _LoadCommand(ThongTinChiTietCuaMotBacSIView p)
        {

        }
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
            parameter.btnSave.IsEnabled = true;

        }
        void _SaveCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            string maBS = parameter.MaBstxb.Text.Substring(2);
            int a = int.Parse(maBS);
            BACSI t = DataProvider.Ins.DB.BACSIs.SingleOrDefault(h => h.MaBS == a);

            MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu lại thông tin bác sĩ không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                t.HoTen = parameter.HoTentxb.Text;
                t.SDT = parameter.SDTtxb.Text;
                t.Email = parameter.Emailtxb.Text;
                t.BangCap = parameter.BangCaptxb.Text;
                t.NgayVaoLam = parameter.NgayVaoLamtxb.SelectedDate.Value;
                t.NgaySinh = parameter.Ngaysinhtxb.SelectedDate;
                t.GioiTinh = parameter.GioiTinhtxb.SelectedValue.ToString();
                t.DiaChi= parameter.DiaChitxb.Text;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã lưu thông tin bác sĩ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                // Xử lý trường hợp không thể ép kiểu thành công (giá trị không hợp lệ)
                // Ví dụ: Hiển thị thông báo lỗi cho người dùng
            }

        }
        void _DeleteCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            string maBS = parameter.MaBstxb.Text.Substring(2);
            int a = int.Parse(maBS);
            BACSI selectedItem = DataProvider.Ins.DB.BACSIs.SingleOrDefault(h => h.MaBS == a);
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bác sĩ này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    // Remove related NGUOIDUNG records
                    var relatedNguoidungs = DataProvider.Ins.DB.NGUOIDUNGs.Where(n => n.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.NGUOIDUNGs.RemoveRange(relatedNguoidungs);
                   
                    // Remove related PHIEUKHAMBENH records
                    var relatedPHIEUKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.PHIEUKHAMBENHs.RemoveRange(relatedPHIEUKBs);
                    var relatedMaPKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBS == selectedItem.MaBS).Select(pkb => pkb.MaPKB).ToList();
                    var relatedCT_PKBs = DataProvider.Ins.DB.CT_PKB.Where(ctpkb => relatedMaPKBs.Contains(ctpkb.MaPKB)).ToList();
                    DataProvider.Ins.DB.CT_PKB.RemoveRange(relatedCT_PKBs);

                    // Remove the selected BACSI
                    DataProvider.Ins.DB.BACSIs.Remove(selectedItem);

                    // Save changes to the database
                    DataProvider.Ins.DB.SaveChanges();

                    // Remove the item from the list
                    listBS.Remove(selectedItem);
                    DanhSachBacSiView danhSachBacSiView = new DanhSachBacSiView();
                    danhSachBacSiView.ListViewBS.Items.Refresh();

                }
            }
            parameter.Hide();

        }
        void Close(ThongTinChiTietCuaMotBacSIView p)
        {
           p.Close();
        }
    }
}
