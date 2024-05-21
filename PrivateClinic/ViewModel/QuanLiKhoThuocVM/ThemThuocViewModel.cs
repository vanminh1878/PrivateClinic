using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemThuocViewModel : BaseViewModel
    {
        public static ThemThuocViewModel Instance { get; } = new ThemThuocViewModel();
        public NhapThuocView AddThuocView { get; set; }
        public ICommand AddMedicineCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand quitCommand { get; set; }
        public ICommand SetAddThuocView { get; set; }
        public ICommand LoadCommand { get; set; }

        //private ThemThuocMoiView _themThuocMoiView;

        //// Constructor
        //public ThemThuocViewModel(ThemThuocMoiView wd)
        //{
        //    this._themThuocMoiView = wd;
        //    AddMedicineCommand = new RelayCommand<object>(CanExecuteAdd, ExecuteAdd);
        //    quitCommand = new ViewModelCommand(quit);
        //}
        private UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public ICommand SwitchViewCommand { get; set; }
        public ThemThuocViewModel()
        {
            SaveCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _SaveCommand(p));
            ExitCommand = new RelayCommand<NhapThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            SetAddThuocView = new RelayCommand<NhapThuocView>((p) => true, (p) => _SetAddThuocView(p));
            LoadCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _LoadCommand(p));
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Thuoccu":

                    CurrentView = new ThemThuocCuView();
                    break;
                case "Thuocmoi":
                    CurrentView = new ThemThuocMoiView();
                    break;
            }
        }
        public void _SetAddThuocView(NhapThuocView view)
        {
            if (AddThuocView == null)
            {
                AddThuocView = view;
            }
        }

        private ObservableCollection<DVT> _dvt;
        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<string> _tenDVTs;
        public ObservableCollection<string> tenDVTs
        {
            get { return _tenDVTs; }
            set
            {
                _tenDVTs = value;
                OnPropertyChanged(nameof(tenDVTs));
            }
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
        void _LoadCommand(ThemThuocMoiView p)
        {
            // Lấy danh sách các TenDVT từ dvt
            var tenDVTs = dvt.Select(x => x.TenDVT);

            // Gán danh sách TenDVT vào các ComboBoxItem của p.TenDVT
            p.TenDVT.ItemsSource = tenDVTs;


        }
        private void _ExitCommand(NhapThuocView p)
        {
            p.Close();
        }

        private string maLoaiThuoc;
        public string MaLoaiThuoc
        {
            get => maLoaiThuoc;
            set
            {
                maLoaiThuoc = value;
                ValidateMaLoaiThuoc();
            }
        }

        private string maLoaiThuocError;
        public string MaLoaiThuocError
        {
            get => maLoaiThuocError;
            set
            {
                maLoaiThuocError = value;
                OnPropertyChanged(nameof(MaLoaiThuocError));
            }
        }

        private string tenThuoc;
        public string TenThuoc
        {
            get => tenThuoc;
            set
            {
                tenThuoc = value;
                ValidateTenThuoc();
            }
        }

        private string tenThuocError;
        public string TenThuocError
        {
            get => tenThuocError;
            set
            {
                tenThuocError = value;
                OnPropertyChanged(nameof(TenThuocError));
            }
        }

        private string maDVT;
        public string MaDVT
        {
            get => maDVT;
            set
            {
                maDVT = value;
                ValidateMaDVT();
            }
        }

        private string maDVTError;
        public string MaDVTError
        {
            get => maDVTError;
            set
            {
                maDVTError = value;
                OnPropertyChanged(nameof(MaDVTError));
            }
        }

        private string maCachDung;
        public string MaCachDung
        {
            get => maCachDung;
            set
            {
                maCachDung = value;
                ValidateMaCachDung();
            }
        }

        private string maCachDungError;
        public string MaCachDungError
        {
            get => maCachDungError;
            set
            {
                maCachDungError = value;
                OnPropertyChanged(nameof(MaCachDungError));
            }
        }

        private string donGiaNhap;
        public string DonGiaNhap
        {
            get => donGiaNhap;
            set
            {
                donGiaNhap = value;
                ValidateDonGiaNhap();
            }
        }

        private string donGiaNhapError;
        public string DonGiaNhapError
        {
            get => donGiaNhapError;
            set
            {
                donGiaNhapError = value;
                OnPropertyChanged(nameof(DonGiaNhapError));
            }
        }

        private string donGiaBan;
        public string DonGiaBan
        {
            get => donGiaBan;
            set
            {
                donGiaBan = value;
                ValidateDonGiaBan();
            }
        }

        private string donGiaBanError;
        public string DonGiaBanError
        {
            get => donGiaBanError;
            set
            {
                donGiaBanError = value;
                OnPropertyChanged(nameof(DonGiaBanError));
            }
        }

        private bool[] _canAccept = new bool[6];



        // Validation methods
        private void ValidateMaLoaiThuoc()
        {
            _canAccept[0] = !string.IsNullOrEmpty(MaLoaiThuoc);
            MaLoaiThuocError = _canAccept[0] ? string.Empty : "Mã loại thuốc không được để trống";
        }

        private void ValidateTenThuoc()
        {
            _canAccept[1] = !string.IsNullOrEmpty(TenThuoc);
            TenThuocError = _canAccept[1] ? string.Empty : "Tên thuốc không được để trống";
        }

        private void ValidateMaDVT()
        {
            _canAccept[2] = !string.IsNullOrEmpty(MaDVT);
            MaDVTError = _canAccept[2] ? string.Empty : "Đơn vị tính không được để trống";
        }

        private void ValidateMaCachDung()
        {
            _canAccept[3] = !string.IsNullOrEmpty(MaCachDung);
            MaCachDungError = _canAccept[3] ? string.Empty : "Cách dùng không được để trống";
        }

        private void ValidateDonGiaNhap()
        {
            _canAccept[4] = double.TryParse(DonGiaNhap, out _);
            DonGiaNhapError = _canAccept[4] ? string.Empty : "Đơn giá nhập không hợp lệ";
        }

        private void ValidateDonGiaBan()
        {
            _canAccept[5] = double.TryParse(DonGiaBan, out _);
            DonGiaBanError = _canAccept[5] ? string.Empty : "Đơn giá bán không hợp lệ";
        }




        void _SaveCommand(ThemThuocMoiView parameter)
        {
            var p = new ThemThuocMoiView();
            p.TenThuoc.Text = ThemThuocMoiView.Instance.EditThuocView.TenThuoc.Text;
            p.NgayNhap.SelectedDate = ThemThuocMoiView.Instance.EditThuocView.NgayNhap.SelectedDate;
            p.DonGiaNhap.Text = ThemThuocMoiView.Instance.EditThuocView.DonGiaNhap.Text;
            p.TenDVT.Text = ThemThuocMoiView.Instance.EditThuocView.TenDVT.Text;
            p.SoLuong.Text = ThemThuocMoiView.Instance.EditThuocView.SoLuong.Text;
            p.MaThuoc.Text = ThemThuocMoiView.Instance.EditThuocView.MaThuoc.Text;

            if (string.IsNullOrEmpty(p.TenThuoc.Text) || p.NgayNhap.SelectedDate == null || string.IsNullOrEmpty(p.DonGiaNhap.Text) || string.IsNullOrEmpty(p.TenDVT.SelectedItem.ToString()) || string.IsNullOrEmpty(p.SoLuong.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn muốn lưu thông tin thuốc?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int maThuoc = int.Parse(p.MaThuoc.Text);
                    THUOC thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(t => t.MaThuoc == maThuoc);

                    if (thuoc != null)
                    {
                        thuoc.TenThuoc = p.TenThuoc.Text;
                        //thuoc.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;
                        thuoc.DonGiaNhap = double.Parse(p.DonGiaNhap.Text);
                        //thuoc.TenDVT = p.TenDVT.Text;
                        thuoc.SoLuong = int.Parse(p.SoLuong.Text);

                        // Tìm hoặc thêm mới đơn vị tính (DVT)
                        //var dvt = DataProvider.Ins.DB.DVTs.FirstOrDefault(d => d.TenDVT == p.TenDVT.Text);

                        var dvtinh = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
                        DVT dvt = dvtinh.FirstOrDefault(d => d.MaDVT == thuoc.MaDVT);
                        dvt.TenDVT = p.TenDVT.Text;

                        ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
                        phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);

                        //var ngaynhap = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.FirstOrDefault(n => n.NgayNhap == p.NgayNhap.SelectedDate);

                        if (ctphieunhap != null)
                        {
                            CT_PNT ct = ctphieunhap.FirstOrDefault(t => t.MaThuoc == maThuoc);
                            if (ct != null)
                            {
                                PHIEUNHAPTHUOC pnt = phieunhap.FirstOrDefault(a => a.SoPhieuNhap == ct.SoPhieuNhap);
                                if (pnt != null)
                                {
                                    pnt.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;
                                }
                                else
                                {
                                    pnt.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;

                                }
                            }
                        }

                        //    DateTime ngayNhap = p.NgayNhap.SelectedDate.Value;
                        //var phieuNhap = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.FirstOrDefault(pn => pn.NgayNhap == ngayNhap);

                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin thuốc thành công!", "THÔNG BÁO");


                        QuanLiKhoThuocView quanLiThuocView = new QuanLiKhoThuocView();
                        quanLiThuocView.MedicineListView.ItemsSource = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                        quanLiThuocView.MedicineListView.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thuốc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ClearFields()
        {
            MaLoaiThuoc = string.Empty;
            TenThuoc = string.Empty;
            MaDVT = string.Empty;
            MaCachDung = string.Empty;
            DonGiaNhap = string.Empty;
            DonGiaBan = string.Empty;
        }


    }
}
