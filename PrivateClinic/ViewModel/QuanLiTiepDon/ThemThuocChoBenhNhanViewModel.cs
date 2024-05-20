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
namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    
    public class ThemThuocChoBenhNhanViewModel:BaseViewModel
    {
        #region Các property và Command
        public ObservableCollection<ThuocDTO> ListThuocDTO;
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


        public ICommand CancelCommand { get; set; }

        #endregion
        public ThemThuocChoBenhNhanViewModel()
        {
            ListThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            ListCachDung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNG);
            ListDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVT);
            CancelCommand = new RelayCommand<ThemThuocChoBenhNhanView>((p) => true, (p) => _CancelCommand(p));
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

        

    }

}

    