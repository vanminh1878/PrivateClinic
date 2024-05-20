using PrivateClinic.Model;
using PrivateClinic.View.ThanhToan;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static PrivateClinic.ViewModel.ThanhToan.ThanhToanViewModel;

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
                if (_currentHoaDon != value)
                {
                    _currentHoaDon = value;
                    OnPropertyChanged(nameof(CurrentHoaDon));


                    if (PhieuKhamBenh == null)
                    {
                        Debug.WriteLine("Không tìm thấy PHIEUKHAMBENH với MaPKB: " + _currentHoaDon.MaPKB);
                    }
                    else
                    {
                        Debug.WriteLine("Tìm thấy PHIEUKHAMBENH, Ngày Khám: " + PhieuKhamBenh.NgayKham);
                    }

                    UpdateMedicationList();
                }
            }
        }


        private PHIEUKHAMBENH _phieuKhamBenh;
        public PHIEUKHAMBENH PhieuKhamBenh
        {
            get => _phieuKhamBenh;
            set
            {
                _phieuKhamBenh = value;
                OnPropertyChanged(nameof(PhieuKhamBenh));
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
            if (CurrentHoaDon != null && CurrentHoaDon.MaPKB != null)
            {
                var phieuKhamBenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.FirstOrDefault(pkb => pkb.MaPKB == CurrentHoaDon.MaPKB);

                var listCT_PKB = DataProvider.Ins.DB.CT_PKB.Where(ct => ct.MaPKB == CurrentHoaDon.MaPKB).ToList();

                foreach (var ctPkb in listCT_PKB)
                {
                    var thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(t => t.MaThuoc == ctPkb.MaThuoc);
                    if (thuoc != null && ctPkb.SoLuong.HasValue)
                    {
                        ListThuoc.Add(new MedicationDetails
                        {
                            Thuoc = thuoc,
                            SoLuong = ctPkb.SoLuong.Value,
                            TienThuoc = (float)(ctPkb.SoLuong.Value * (thuoc.DonGiaBan ?? 0))
                        });
                    }
                }
            }
        }

        void _SaveCommand(HoaDonView parameter)
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
                    invoiceToUpdate.TrangThai = ((int)PaymentStatus.Paid).ToString();
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thanh toán thành công!", "THÔNG BÁO");
                    parameter.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Hóa đơn không tồn tại.", "Error");
                }
                parameter.Close();

            }
        }

        void _CancelCommand(HoaDonView parameter)
        {
            parameter.Close();
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
