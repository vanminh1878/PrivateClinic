using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class SuaThongTinDonThuocViewModel:BaseViewModel
    {
        public SuaThongTinDonThuocView EditMedView { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private ObservableCollection<ThuocDTO> listthuocDTO;
        public ObservableCollection<ThuocDTO> ListThuocDTO
        {
            get => listthuocDTO;
            set
            {
                listthuocDTO = value;
                OnPropertyChanged(nameof(ListThuocDTO));
                
            }
        }
        private ObservableCollection<THUOC> listthuoc;
        public ObservableCollection<THUOC> ListThuoc
        {
            get { return listthuoc; }
            set
            {
                listthuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }

        private THUOC selectedThuoc;
        public THUOC SelectedThuoc
        {
            get => selectedThuoc;
            set
            {
                if (selectedThuoc != value)
                {
                    selectedThuoc = value;
                    OnPropertyChanged(nameof(SelectedThuoc));
                    XacDinhTenDonVi();
                    XacDinhMaThuoc();
                }
            }
        }

        private ObservableCollection<CACHDUNG> listcachdung;
        public ObservableCollection<CACHDUNG> ListCachDung
        {
            get => listcachdung;
            set
            {
                listcachdung = value;
                OnPropertyChanged(nameof(ListCachDung));
            }
        }

        private CACHDUNG selectedCachDung;
        public CACHDUNG SelectedCachDung
        {
            get => selectedCachDung;
            set
            {
                selectedCachDung = value;
                OnPropertyChanged(nameof(SelectedCachDung));
            }
        }

        private ObservableCollection<DVT> listDVT;
        public ObservableCollection<DVT> ListDVT
        {
            get => listDVT;
            set
            {
                listDVT = value;
                OnPropertyChanged(nameof(ListDVT));
            }
        }
        private string donvi;
        public string DonVi
        {
            get => donvi;
            set
            {
                donvi = value;
                OnPropertyChanged(nameof(DonVi));
            }
        }

        private string mathuoc;
        public string MaThuoc
        {
            get => mathuoc;
            set
            {
                mathuoc = value;
                OnPropertyChanged(nameof(MaThuoc));
            }
        }

        private string soluong;
        public string SoLuong
        {
            get => soluong;
            set
            {
                soluong = value;
                OnPropertyChanged(nameof(SoLuong));
            }
        }
        public ThuocDTO thuocDTO { get; set; }
        private SuaThongTinDonThuocView _view;
        public SuaThongTinDonThuocViewModel(SuaThongTinDonThuocView view)
        {
            this._view = view;
            ListThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            ListCachDung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNG);
            ListDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVT);
            CancelCommand = new RelayCommand<SuaThongTinDonThuocView>((p) => true, (p) => _CancelCommand(p));
            SaveCommand = new ViewModelCommand(acceptSave);
        }
        void _CancelCommand(SuaThongTinDonThuocView paramater)
        {
            paramater.Close();
        }
        private void XacDinhTenDonVi()
        {
            if (SelectedThuoc != null)
            {
                var thuoc = ListDVT.FirstOrDefault(dvt => dvt.MaDVT == SelectedThuoc.MaDVT);
                DonVi = thuoc != null ? thuoc.TenDVT : string.Empty;
            }
            else
            {
                DonVi = "";
            }
        }

        private void XacDinhMaThuoc()
        {
            if (SelectedThuoc != null)
            {
                var thuoc = ListThuoc.FirstOrDefault(ma => ma.MaThuoc == selectedThuoc.MaThuoc);
                MaThuoc = thuoc.MaThuoc.ToString();
            }
            else
            {
                MaThuoc = "";
            }
        }
        //Hàm load thông tin hiện tại của item đang được chọn
        public void LoadEditCurrent()
        {
            if (thuocDTO != null)
            {
                // Tìm và gán thuốc được chọn dựa trên MaThuoc của thuocDTO
                SelectedThuoc = ListThuoc.FirstOrDefault(t => t.MaThuoc.ToString() == thuocDTO.MaThuoc);

                // Tìm và gán cách dùng dựa trên tên cách dùng của thuocDTO
                SelectedCachDung = ListCachDung.FirstOrDefault(cd => cd.TenCachDung == thuocDTO.CachDung);
                MaThuoc = thuocDTO.MaThuoc;
                // Gán các thuộc tính còn lại
                DonVi = thuocDTO.DonVi;
                SoLuong = thuocDTO.SoLuong;
            }
            else
            {
                // Nếu thuocDTO là null, reset các thuộc tính về giá trị mặc định
                SelectedThuoc = null;
                SelectedCachDung = null;
                DonVi = string.Empty;
                SoLuong = string.Empty;
            }
        }

        //Hàm lưu thông tin thuốc
        void acceptSave(object obj)
        {
            if (SelectedThuoc == null || SelectedCachDung == null || string.IsNullOrEmpty(SoLuong))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!SoLuong.All(char.IsDigit))
            {
                MessageBox.Show("Số lượng chỉ chứa số", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn lưu thông tin thuốc ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    ThuocDTO thuoc = Const.ListThuocTemp.FirstOrDefault(t => t.MaThuoc == thuocDTO.MaThuoc);
                    thuoc.MaThuoc = MaThuoc;
                    thuoc.DonVi = DonVi;
                    thuoc.SoLuong = SoLuong;
                    thuoc.TenThuoc = SelectedThuoc.TenThuoc;
                    thuoc.CachDung = SelectedCachDung.TenCachDung;
                    MessageBox.Show("Thành công", "Thông báo");
                    _view.Close();
                }
            }
            
        }
    }
}
