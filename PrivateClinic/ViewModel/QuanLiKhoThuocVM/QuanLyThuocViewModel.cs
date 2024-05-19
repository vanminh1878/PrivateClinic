using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class QuanLyThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _listMedicine;

        public ObservableCollection<THUOC> ListMedicine
        {
            get => _listMedicine;
            set { _listMedicine = value; OnPropertyChanged(nameof(ListMedicine)); }

        }


        public ICommand AddMedicineCommand { get; set; }
        public ICommand EditMedicineCommand { get; set; }
        public ICommand DeleteMedicineCommand { get; set; }

        public QuanLyThuocViewModel()
        {
            LoadMedicines();
            AddMedicineCommand = new RelayCommand<QuanLiKhoThuocView>((p) => { return p == null ? false : true; }, (p) => _AddMedicineCommand(p));
            EditMedicineCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _EditMedicineCommand(p));
            DeleteMedicineCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _DeleteMedicineCommand(p));
        }

        private void LoadMedicines()
        {

            var context = DataProvider.Ins.DB;
            var thuocList = from t in context.THUOCs
                            join d in context.DVTs on t.MaDVT equals d.MaDVT
                            select new
                            {
                                t.MaThuoc,
                                t.TenThuoc,
                                t.DonGiaNhap,
                                t.SoLuong,
                                d.TenDVT
                            };

            ListMedicine = new ObservableCollection<THUOC>(
            thuocList.ToList().Select((t, index) => new THUOC
            {
                STT = index + 1,
                MaThuoc = t.MaThuoc,
                TenThuoc = t.TenThuoc,
                TenDVT = t.TenDVT,
                DonGiaNhap = t.DonGiaNhap,
                SoLuong = t.SoLuong
            }));
        }
        void _EditMedicineCommand(THUOC selectedItem)
        {
            if (selectedItem != null)
            {
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView = new ThayDoiThongTinThuocView();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenThuoc.Text = selectedItem.TenThuoc.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.NgayNhap.Text = selectedItem.NgayNhap.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.DonGiaNhap.Text = selectedItem.DonGiaNhap.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenDVT.Text = selectedItem.TenDVT.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.SoLuong.Text = selectedItem.SoLuong.ToString();

                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.ShowDialog();

            }
        }

        private void _DeleteMedicineCommand(THUOC selectedItem)
        {
            MessageBoxResult r = MessageBox.Show("Bạn muốn xóa thuốc này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    // Thực hiện xóa thuốc
                    var context = DataProvider.Ins.DB;
                    // Xóa thuốc
                    context.THUOCs.Remove(selectedItem);
                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                    // Xóa khỏi danh sách thuốc
                    ListMedicine.Remove(selectedItem);
                }
            }
        }
        public void _AddMedicineCommand(QuanLiKhoThuocView parameter)
        {
            ThemThuocMoiView addMedicineView = new ThemThuocMoiView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            addMedicineView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addMedicineView.ShowDialog();

        }


    }
}
