using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemThuocViewModel : BaseViewModel
    {

        public ICommand AddMedicineCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand quitCommand { get; set; }

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
            AddMedicineCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => ExecuteAdd(p));
            ExitCommand = new RelayCommand<NhapThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));

            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Thuoccu":
                    
                    CurrentView= new ThemThuocCuView();
                    break;
                case "Thuocmoi":
                    CurrentView = new ThemThuocMoiView();
                    break;
            }
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


        private bool CanExecuteAdd(object parameter)
        {
            foreach (bool can in _canAccept)
            {
                if (!can) return false;
            }
            return true;
        }

        void ExecuteAdd(ThemThuocMoiView parameter)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn thêm thuốc mới?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    THUOC thuoc = new THUOC
                    {
                        MaLoaiThuoc = int.Parse(MaLoaiThuoc),
                        TenThuoc = TenThuoc,
                        MaDVT = int.Parse(MaDVT),
                        MaCachDung = int.Parse(MaCachDung),
                        DonGiaNhap = double.Parse(DonGiaNhap),
                        DonGiaBan = double.Parse(DonGiaBan)
                    };

                    DataProvider.Ins.DB.THUOCs.Add(thuoc);
                    DataProvider.Ins.DB.SaveChanges();

                    QuanLiKhoThuocView quanlikhothuocview = new QuanLiKhoThuocView();
                    quanlikhothuocview.MedicineListView.ItemsSource = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                    quanlikhothuocview.MedicineListView.Items.Refresh();

                    MessageBox.Show("Thêm thuốc mới thành công", "Thông báo");
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
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
