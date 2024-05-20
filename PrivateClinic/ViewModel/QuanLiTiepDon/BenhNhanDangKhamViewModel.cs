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
    public class BenhNhanDangKhamViewModel: BaseViewModel
    {
        private ObservableCollection<THUOC> _listMed;
        public ObservableCollection<THUOC> listMed { get => _listMed; set { _listMed = value; OnPropertyChanged(); } }

        //Lấy danh sách cách dùng thuốc DTO
        private ObservableCollection<ThuocDTO> listThuoc;
        public ObservableCollection<ThuocDTO> ListThuoc
        {
            get => listThuoc;
            set
            {
                listThuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditMedCommand { get; set; }

        public BenhNhanDangKhamViewModel()
        {
            listMed = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            SearchCommand = new RelayCommand<BenhNhanDangKhamView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DeleteCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            AddCommand = new RelayCommand<BenhNhanDangKhamView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            EditMedCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _EditMedCommand(p));
        }
        void _AddCommand(BenhNhanDangKhamView parameter)
        {

            ThemThuocChoBenhNhanView themThuocChoBenhNhanView = new ThemThuocChoBenhNhanView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            themThuocChoBenhNhanView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            themThuocChoBenhNhanView.ShowDialog();

        }
        void _EditMedCommand(THUOC selectedItem)
        {
            if (selectedItem != null)
            {

                SuaThongTinDonThuocViewModel.Instance.EditMedView = new SuaThongTinDonThuocView();
                SuaThongTinDonThuocViewModel.Instance.EditMedView.TenThuoc.SelectedIndex = SuaThongTinDonThuocViewModel.Instance.EditMedView.TenThuoc.Items.IndexOf(selectedItem.TenThuoc.ToString());

                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                SuaThongTinDonThuocViewModel.Instance.EditMedView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                SuaThongTinDonThuocViewModel.Instance.EditMedView.ShowDialog();

            }
        }
        void _SearchCommand(BenhNhanDangKhamView parameter)
        {
            ObservableCollection<THUOC> temp = new ObservableCollection<THUOC>();
            if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
            {
                string searchKeyword = parameter.txbSearch.Text.ToLower();
                temp = new ObservableCollection<THUOC>(listMed.Where(s => s.TenThuoc.ToLower().Contains(searchKeyword)));
            }
            else
            {
                temp = new ObservableCollection<THUOC>(listMed);
            }
            parameter.ListViewMed.ItemsSource = temp;
        }
        void _DeleteCommand(THUOC selectedItem)
        {
            //MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bệnh nhân này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //if (r == MessageBoxResult.Yes)
            //{
            //    if (selectedItem != null)
            //    {
            //        // Remove related HOADON records
            //        var relatedHoadons = DataProvider.Ins.DB.HOADONs.Where(h => h.MaBN == selectedItem.MaBN).ToList();
            //        DataProvider.Ins.DB.HOADONs.RemoveRange(relatedHoadons);
            //        // Remove related CT_BCDT records
            //        var relatedCTBCDTs = DataProvider.Ins.DB.CT_BCDT.Where(ct => ct.HOADON.MaBN == selectedItem.MaBN).ToList();
            //        DataProvider.Ins.DB.CT_BCDT.RemoveRange(relatedCTBCDTs);
            //        // Remove related PHIEUKHAMBENH records
            //        var relatedPHIEUKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).ToList();
            //        DataProvider.Ins.DB.PHIEUKHAMBENHs.RemoveRange(relatedPHIEUKBs);
            //        var relatedMaPKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).Select(pkb => pkb.MaPKB).ToList();
            //        var relatedCT_PKBs = DataProvider.Ins.DB.CT_PKB.Where(ctpkb => relatedMaPKBs.Contains(ctpkb.MaPKB)).ToList();
            //        DataProvider.Ins.DB.CT_PKB.RemoveRange(relatedCT_PKBs);

            //        // Remove the selected BENHNHAN
            //        DataProvider.Ins.DB.BENHNHANs.Remove(selectedItem);

            //        // Save changes to the database
            //        DataProvider.Ins.DB.SaveChanges();

            //        // Remove the item from the list
            //        listMed.Remove(selectedItem);
       
            //        QuanLiTiepDonView quanLiTiepDonView = new QuanLiTiepDonView();
            //        quanLiTiepDonView.txbSLBNDK.Text = listMed.Count.ToString();
            //        quanLiTiepDonView.txbSLBNDK.FontSize = 25;
            //    }
            //}
        }


    }
}
