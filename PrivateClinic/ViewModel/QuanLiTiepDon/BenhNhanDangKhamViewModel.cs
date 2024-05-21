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

        //Lấy danh sách  thuốc DTO
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

        private ObservableCollection<ThuocDTO> listThuocView;
        public ObservableCollection<ThuocDTO> ListThuocView
        {
            get => listThuocView;
            set
            {
                listThuocView = value;
                OnPropertyChanged(nameof(ListThuocView));
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditThuocCommand { get; set; }

        public BenhNhanDangKhamViewModel()
        {
            listMed = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            AddThuoc();
            EditThuoc();
            ListThuoc = new ObservableCollection<ThuocDTO>();
            SearchCommand = new RelayCommand<BenhNhanDangKhamView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DeleteCommand = new RelayCommand<THUOC>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
        }
        void AddThuoc()
        {
            AddCommand = new ViewModelCommand(showAdd);
        }
        private void showAdd(object obj)
        {
            ThemThuocChoBenhNhanView view = new ThemThuocChoBenhNhanView();
            view.ShowDialog();
            LoadData();
        }
        #region Chức năng sửa 1 bác sĩ
        void EditThuoc()
        {
            EditThuocCommand = new ViewModelCommand(ShowWDEditThuoc);
        }
        private void ShowWDEditThuoc(object obj)
        {
            ThuocDTO thuoc = obj as ThuocDTO;
            SuaThongTinDonThuocView view = new SuaThongTinDonThuocView((ThuocDTO)obj);
            view.ShowDialog();
            LoadData();
        }

        #endregion
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

        void LoadData()
        {
            if (ListThuocView == null)
            {
                ListThuocView = new ObservableCollection<ThuocDTO>();
            }
            // Add items from ListThuoc to ListThuocView
            if (Const.ListThuoc == null)
                return;
            else
            {
                foreach (var thuocDTO in Const.ListThuoc)
                {
                    ListThuocView.Add(thuocDTO);
                }
                Const.ListThuoc.Clear();
            }
            
        }

    }
}
