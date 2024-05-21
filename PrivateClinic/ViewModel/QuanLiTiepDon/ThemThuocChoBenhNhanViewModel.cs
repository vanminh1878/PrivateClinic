using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using PrivateClinic.Model;
using System.Windows;
namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    
    public class ThemThuocChoBenhNhanViewModel:BaseViewModel
    {
        #region Các property và Command
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
        public ObservableCollection <THUOC> ListThuoc
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
                OnPropertyChanged(nameof (SelectedCachDung));
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

        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private ThemThuocChoBenhNhanView _view;
        #endregion
        public ThemThuocChoBenhNhanViewModel(ThemThuocChoBenhNhanView view)
        {
            this._view = view;
            ListThuocDTO = new ObservableCollection<ThuocDTO>();
            ListThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            ListCachDung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNG);
            ListDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVT);
            CancelCommand = new RelayCommand<ThemThuocChoBenhNhanView>((p) => true, (p) => _CancelCommand(p));
            AddCommand = new ViewModelCommand(AcceptAdd);
        }
        void _CancelCommand(ThemThuocChoBenhNhanView paramater)
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
        int stt = 0;
        private void AcceptAdd(object obj)
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
                    ThuocDTO thuocDTO = new ThuocDTO();
                    stt++;
                    thuocDTO.STT = stt;
                    thuocDTO.MaThuoc= MaThuoc;
                    thuocDTO.TenThuoc = selectedThuoc.TenThuoc;
                    thuocDTO.SoLuong = SoLuong;
                    thuocDTO.CachDung = SelectedCachDung.TenCachDung;
                    thuocDTO.DonVi = DonVi;
                    ListThuocDTO.Add(thuocDTO);
                    MessageBox.Show("Đã thêm","Thông báo",MessageBoxButton.OK);
            }
        }





    }

}

    