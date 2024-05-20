using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class QuanLyThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _thuoc;

        public ObservableCollection<THUOC> Thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(Thuoc)); }

        }
        private ObservableCollection<CT_PNT> _ctphieunhap;

        public ObservableCollection<CT_PNT> ctphieunhap
        {
            get => _ctphieunhap;
            set { _ctphieunhap = value; OnPropertyChanged(nameof(ctphieunhap)); }

        }
        private ObservableCollection<PHIEUNHAPTHUOC> _phieunhap;

        public ObservableCollection<PHIEUNHAPTHUOC> phieunhap
        {
            get => _phieunhap;
            set { _phieunhap = value; OnPropertyChanged(nameof(phieunhap)); }

        }
        private ObservableCollection<ThuocDTO> _listMed;

        public ObservableCollection<ThuocDTO> listMed
        {
            get => _listMed;
            set { _listMed = value; OnPropertyChanged(nameof(listMed)); }

        }

        private ObservableCollection<DVT> _donvitinh;

        public ObservableCollection<DVT> donvitinh
        {
            get => _donvitinh;
            set { _donvitinh = value; OnPropertyChanged(nameof(donvitinh)); }

        }



        public ICommand AddMedicineCommand { get; set; }
        public ICommand EditMedicineCommand { get; set; }
        public ICommand DeleteMedicineCommand { get; set; }
        public ICommand DetailMedicineCommand { get; set; }

        public QuanLyThuocViewModel()
        {
            LoadMedicines();
            AddMedicineCommand = new RelayCommand<QuanLiKhoThuocView>((p) => { return p == null ? false : true; }, (p) => _AddMedicineCommand(p));
            EditMedicineCommand = new RelayCommand<ThuocDTO>((p) => { return p == null ? false : true; }, (p) => _EditMedicineCommand(p));
            DeleteMedicineCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _DeleteMedicineCommand(p));
            DetailMedicineCommand = new RelayCommand<QuanLiKhoThuocView>((p) => { return p == null ? false : true; }, (p) => _DetailMedicineCommand(p));
            LoadData();
        }
        void LoadData()
        {
            Thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            donvitinh = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            listMed = new ObservableCollection<ThuocDTO>();
            int stt = 1;
            foreach(var  thuoc in Thuoc)
            {
                foreach(var dvt in donvitinh)
                {
                    ThuocDTO thuocDTO = new ThuocDTO()
                    {
                        STT = stt,
                        DVT = dvt.TenDVT,
                        MaThuoc = thuoc.MaThuoc,
                        TenThuoc = thuoc.TenThuoc,
                        Gia = thuoc.DonGiaNhap,
                        SL= thuoc.SoLuong

                    };
                    listMed.Add(thuocDTO);
                    stt++;
                    break;
                }
            }
        }

        private void LoadMedicines()
        {
            //var context = DataProvider.Ins.DB;
            //var thuocList = from t in context.THUOCs
            //                join d in context.DVTs on t.MaDVT equals d.MaDVT
            //                select new
            //                {
            //                    //t.STT,
            //                    t.MaThuoc,
            //                    t.TenThuoc,
            //                    t.DonGiaNhap,
            //                    //t.SoLuong,
            //                    d.TenDVT
            //                };

            //ListMedicine = new ObservableCollection<THUOC>(
            //thuocList.ToList().Select((t, index) => new THUOC
            //{
            //    //STT = index + 1,
            //    MaThuoc = t.MaThuoc,
            //    TenThuoc = t.TenThuoc,
            //    //TenDVT = t.TenDVT,
            //    DonGiaNhap = t.DonGiaNhap,
            //    //SoLuong = t.SoLuong
            //}));
        }
        void _EditMedicineCommand(ThuocDTO selectedItem)
        {
            if (selectedItem != null)
            {
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView = new ThayDoiThongTinThuocView();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenThuoc.Text = selectedItem.TenThuoc.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.MaThuoc.Text = selectedItem.MaThuoc.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.DonGiaNhap.Text = selectedItem.Gia.ToString();
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.SoLuong.Text = selectedItem.SL.ToString();
                var itemList = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenDVT.Items;
                int index = itemList.IndexOf(selectedItem.DVT.ToString());
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenDVT.SelectedIndex = index;
                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                ThayDoiThongTinThuocViewModel.Instance.EditThuocView.ShowDialog();

            }
        }

        private void _DeleteMedicineCommand(THUOC selectedItem)
        {
            //MessageBoxResult r = MessageBox.Show("Bạn muốn xóa thuốc này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //if (r == MessageBoxResult.Yes)
            //{
            //    if (selectedItem != null)
            //    {
            //        // Thực hiện xóa thuốc
            //        var context = DataProvider.Ins.DB;
            //        // Xóa thuốc
            //        context.THUOCs.Remove(selectedItem);
            //        // Lưu thay đổi vào cơ sở dữ liệu
            //        context.SaveChanges();
            //        // Xóa khỏi danh sách thuốc
            //        listMed.Remove(selectedItem);
            //    }
            //}
        }
        public void _AddMedicineCommand(QuanLiKhoThuocView parameter)
        {
            ThemThuocMoiView addMedicineView = new ThemThuocMoiView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            addMedicineView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addMedicineView.ShowDialog();

        }
        public void _DetailMedicineCommand(QuanLiKhoThuocView parameter)
        {
            ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
            phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);
            ThongTinThuocView detail = new ThongTinThuocView();
            ThuocDTO selectedThuocDTO = (ThuocDTO)parameter.MedicineListView.SelectedItem;
            THUOC temp = Thuoc.FirstOrDefault(t => t.MaThuoc == selectedThuocDTO.MaThuoc);
            detail.DonGia.Text = temp.DonGiaNhap.ToString();
            detail.TenThuoc.Text = temp.TenThuoc;
            detail.MaThuoc.Text = temp.MaThuoc.ToString();
            detail.DVT.Text = temp.DVT.TenDVT.ToString();
            detail.SL.Text = temp.SoLuong.ToString();
            if (ctphieunhap != null)
            {
                CT_PNT ct = ctphieunhap.FirstOrDefault(t => t.MaThuoc == temp.MaThuoc);
                if (ct != null)
                {
                    PHIEUNHAPTHUOC pnt = phieunhap.FirstOrDefault(a => a.SoPhieuNhap == ct.SoPhieuNhap);
                    if (pnt != null)
                    {
                        detail.NgNhap.Text = pnt.NgayNhap.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        // Xử lý trường hợp pnt là null
                        detail.NgNhap.Text = "Chưa có ngày nhập";
                    }
                }
                else
                {
                    // Xử lý trường hợp ct là null
                    detail.NgNhap.Text = "Chưa có ngày nhập";
                }
            }
            else
            {
                // Xử lý trường hợp ctphieunhap là null
                detail.NgNhap.Text = "Chưa có ngày nhập";
            }
            detail.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            detail.ShowDialog();
        }


    }
}
