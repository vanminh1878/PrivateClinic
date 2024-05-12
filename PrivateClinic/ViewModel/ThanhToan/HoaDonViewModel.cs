using PrivateClinic.Model;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.View.ThanhToan;
using System.Windows.Documents;

namespace PrivateClinic.ViewModel.ThanhToan
{
    public class HoaDonViewModel : BaseViewModel
    {
        #region Properties
        public static HoaDonViewModel Instance { get; } = new HoaDonViewModel();

        public HoaDonView hoaDonView { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetHoaDonView { get; set; }

        private HOADON _currentHoaDon;
        public HOADON CurrentHoaDon
        {
            get => _currentHoaDon;
            set
            {
                _currentHoaDon = value;
                OnPropertyChanged(nameof(CurrentHoaDon));
                UpdateMedicationList();
            }
        }

        private ObservableCollection<MedicationDetails> _listThuoc;
        public ObservableCollection<MedicationDetails> ListThuoc
        {
            get => _listThuoc;
            set
            {
                _listThuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }
        #endregion

        public HoaDonViewModel()
        {
            ListThuoc = new ObservableCollection<MedicationDetails>();
            SaveCommand = new RelayCommand<HoaDonView>((p) => true, (p) => _SaveCommand(p));
            CancelCommand = new RelayCommand<HoaDonView>((p) => true, p => _CancelCommand(p));
        }

        private void UpdateMedicationList()
        {
            ListThuoc.Clear();
            if (CurrentHoaDon != null && CurrentHoaDon.PHIEUKHAMBENH != null)
            {
                foreach (var ctPkb in CurrentHoaDon.PHIEUKHAMBENH.CT_PKB)
                {
                    if (ctPkb.THUOC != null && ctPkb.SoLuong.HasValue)
                    {
                        ListThuoc.Add(new MedicationDetails
                        {
                            Thuoc = ctPkb.THUOC,
                            SoLuong = ctPkb.SoLuong.Value,
                            TienThuoc = (float)(ctPkb.SoLuong.Value * (ctPkb.THUOC.DonGiaBan ?? 0)) 
                        });
                    }
                }
            }
        }

        void _SaveCommand(HoaDonView paramater)
        {
            if (CurrentHoaDon == null)
            {
                MessageBox.Show("No invoice selected.", "Error");
                return;
            }
            MessageBoxResult confirmation = MessageBox.Show("Xác nhận thanh toán hóa đơn này?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                HOADON invoiceToUpdate = DataProvider.Ins.DB.HOADONs.FirstOrDefault(hd => hd.SoHD == CurrentHoaDon.SoHD);
                if (invoiceToUpdate != null)
                {
                    //TODO update daThanhToan
                    //invoiceToUpdate.daThanhToan = true; 
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thanh toán thành công!", "THÔNG BÁO");
                    ThanhToanView thanhToanView = new ThanhToanView();
                    thanhToanView.ListViewHD.ItemsSource = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
                    thanhToanView.ListViewHD.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Hóa đơn không tồn tại.", "Error");
                }

            }
        }

        void _CancelCommand(HoaDonView paramater)
        {
            paramater.Close();
        }
    }
    public class MedicationDetails : BaseViewModel
    {
        private THUOC _thuoc;
        public THUOC Thuoc
        {
            get => _thuoc;
            set
            {
                _thuoc = value;
                OnPropertyChanged(nameof(Thuoc));
                OnPropertyChanged(nameof(TienThuoc));
            }
        }

        private int _soLuong;
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                _soLuong = value;
                OnPropertyChanged(nameof(SoLuong));
                OnPropertyChanged(nameof(TienThuoc));
            }
        }

        private float _tienThuoc;
        public float TienThuoc
        {
            get => _tienThuoc;
            set
            {
                _tienThuoc = value;
                OnPropertyChanged(nameof(TienThuoc));
            }
        }
    }
}
