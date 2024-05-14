using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.ThanhToan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using System.Windows.Documents;

namespace PrivateClinic.ViewModel.ThanhToan
{
    public class ThanhToanViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { _listHD = value; OnPropertyChanged(); } }

        private PaymentStatus _selectedPaymentStatus = PaymentStatus.All;
        public PaymentStatus SelectedPaymentStatus
        {
            get => _selectedPaymentStatus;
            set
            {
                _selectedPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedPaymentStatus));
                FilterListHD();
            }
        }

        private HOADON _hoaDon;
        public HOADON HoaDon
        {
            get => _hoaDon;
            set
            {
                _hoaDon = value;
                OnPropertyChanged(nameof(HoaDon));
            }
        }

        public HoaDonView hoaDonView { get; set; }
        public ICommand SearchCommand { get; set; }


        public ICommand LoadCommand { get; set; }
        public ICommand LoadSLHDCommand { get; set; }

        //LISTVIEW COMMAND
        public ICommand PayHDCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public enum PaymentStatus
        {
            All = -1,
            Unpaid = 0,
            Paid = 1
        }

        #endregion

        public ThanhToanViewModel()
        {
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<ThanhToanView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            LoadSLHDCommand = new RelayCommand<ThanhToanView>((p) => { return p == null ? false : true; }, (p) => _LoadSLHDCommand(p));
            PayHDCommand = new RelayCommand<HOADON>((p) => { return p == null ? false : true; }, (p) => _PayHDCommand(p));
            DeleteCommand = new RelayCommand<HOADON>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
        }

        #region Functions
        void _LoadSLHDCommand(ThanhToanView parameter)
        {
            parameter.txbSLHD.Text = listHD.Count.ToString();
            parameter.txbSLHD.FontSize = 25;
        }

        void _SearchCommand(ThanhToanView parameter)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
            {
                string searchKeyword = parameter.txbSearch.Text.ToLower();
                temp = new ObservableCollection<HOADON>(listHD.Where(s => s.BENHNHAN.HoTen.ToLower().Contains(searchKeyword)));
            }
            else
            {
                temp = new ObservableCollection<HOADON>(listHD);
            }
            parameter.ListViewHD.ItemsSource = temp;
        }

        void _DeleteCommand(HOADON selectedItem)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa hóa đơn này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    var relatedHoadons = DataProvider.Ins.DB.HOADONs.Where(h => h.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.HOADONs.RemoveRange(relatedHoadons);
                    var relatedCTBCDTs = DataProvider.Ins.DB.CT_BCDT.Where(ct => ct.HOADON.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.CT_BCDT.RemoveRange(relatedCTBCDTs);
                    var relatedPHIEUKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.PHIEUKHAMBENHs.RemoveRange(relatedPHIEUKBs);
                    var relatedMaPKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).Select(pkb => pkb.MaPKB).ToList();
                    var relatedCT_PKBs = DataProvider.Ins.DB.CT_PKB.Where(ctpkb => relatedMaPKBs.Contains(ctpkb.MaPKB)).ToList();
                    DataProvider.Ins.DB.CT_PKB.RemoveRange(relatedCT_PKBs);

                    DataProvider.Ins.DB.HOADONs.Remove(selectedItem);

                    DataProvider.Ins.DB.SaveChanges();

                    listHD.Remove(selectedItem);
                    ThanhToanView thanhToanView = new ThanhToanView();
                    thanhToanView.txbSLHD.Text = listHD.Count.ToString();
                    thanhToanView.txbSLHD.FontSize = 25;
                }
            }
        }
        public void SetThanhToanHDView(HoaDonView view)
        {
            hoaDonView = view;
        }

        private void FilterListHD()
        {
            var allHoaDons = DataProvider.Ins.DB.HOADONs.ToList();
            if (SelectedPaymentStatus == PaymentStatus.All)
            {
                listHD = new ObservableCollection<HOADON>(allHoaDons);
            }
            else
            {
                var statusString = ((int)SelectedPaymentStatus).ToString();
                listHD = new ObservableCollection<HOADON>(
                    allHoaDons.Where(x => x.TrangThai == statusString));
            }
        }

        void _PayHDCommand(HOADON selectedItem)
        {
            if (selectedItem != null)
            {
                HoaDonViewModel hoaDonVM = new HoaDonViewModel();
                hoaDonVM.CurrentHoaDon = selectedItem;

                HoaDonView hoaDonView = new HoaDonView();
                hoaDonView.DataContext = hoaDonVM;
                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                hoaDonView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                hoaDonView.Height = 550;
                hoaDonView.ShowDialog();
            }
        }
        #endregion
    }
}


