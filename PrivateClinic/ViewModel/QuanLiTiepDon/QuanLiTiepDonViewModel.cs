using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
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
using PrivateClinic.View.HoSoBacSi;


namespace PrivateClinic.ViewModel.QuanLiTiepDon
{

    public class QuanLiTiepDonViewModel : BaseViewModel
    {
        private ObservableCollection<BENHNHAN> _listBN;
        public ObservableCollection<BENHNHAN> listBN { get => _listBN; set { _listBN = value; OnPropertyChanged(); } }
        private ObservableCollection<QuanLiKhamBenhView> _currentView;
        public ObservableCollection<QuanLiKhamBenhView> CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

        public SuaBenhNhanView EditBNView { get; set; }
        public BenhNhanDangKhamView KhamBNView { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }    
        public ICommand LoadCommand { get; set; }    
        public ICommand LoadSLBNCommand { get; set; }    
        public ICommand EditSLBNCommand { get; set; }    
        public ICommand EditUpSLBNCommand { get; set; }    
        public ICommand EditDownSLBNCommand { get; set; }    
        public ICommand SaveSLBNCommand { get; set; }    
        public ICommand EditBNCommand { get; set; }

        public ICommand selectedItemBenhNhan { get; set; }

       
        public QuanLiTiepDonViewModel()
        {
            LoadData();
            SearchCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DeleteCommand = new RelayCommand<BENHNHAN>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            AddCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            LoadCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _LoadCommand(p));
            LoadSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _LoadSLBNCommand(p));
            EditBN();
            UpdateSTT();
            if (Const.PQ.MaNhom == "NHOM1")
            {
                EditSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditSLBNCommand(p));
                EditUpSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditUpSLBNCommand(p));
                EditDownSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditDownSLBNCommand(p));
                SaveSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _SaveSLBNCommand(p));
            }
            
        }

        void UpdateSTT()
        {
            for (int i = 0; i < listBN.Count; i++)
            {
                //listBN[i].STT = i + 1;
            }
        }
        void _LoadCommand(QuanLiTiepDonView parameter)
        {
            parameter.txbSLBNDK.Text = listBN.Count.ToString();
            parameter.txbSLBNDK.FontSize = 25;
            
        }
        void _EditSLBNCommand(QuanLiTiepDonView parameter)
        {
          
            parameter.txbMaxBN.IsEnabled = true;
            parameter.btnUp.IsEnabled = true;
            parameter.btnDown.IsEnabled = true;
            parameter.btnSave.IsEnabled = true;

        }
        void _EditUpSLBNCommand(QuanLiTiepDonView parameter)
        {

            parameter.txbMaxBN.IsEnabled = true;
            if (int.TryParse(parameter.txbMaxBN.Text, out int currentValue))
            {
                int newValue = currentValue + 1; 
                parameter.txbMaxBN.Text = newValue.ToString();
            }


        }
        void _EditDownSLBNCommand(QuanLiTiepDonView parameter)
        {
            
            parameter.txbMaxBN.IsEnabled = true;
            if (int.TryParse(parameter.txbMaxBN.Text, out int currentValue))
            {
                int newValue = currentValue - 1; 
                parameter.txbMaxBN.Text = newValue.ToString();
            }

        }

        //Load số lượng bệnh nhân khám tối đa
        void _LoadSLBNCommand(QuanLiTiepDonView parameter)
        {
            THAMSO t = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 1);
            parameter.txbMaxBN.Text = t.GiaTri.ToString();
            parameter.txbMaxBN.FontSize = 25;
        }

        //Lưu số lượng bệnh nhân tối đa
        void _SaveSLBNCommand(QuanLiTiepDonView parameter)
        {
   
            THAMSO t = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 1);
            if (int.TryParse(parameter.txbMaxBN.Text, out int newValue))
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu lại số bệnh nhân khám tối đa trong ngày không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    t.GiaTri = newValue;
                    DataProvider.Ins.DB.SaveChanges();
                    parameter.txbMaxBN.IsEnabled = false;
                    parameter.btnSave.IsEnabled = false;
                    MessageBox.Show("Đã lưu số bệnh nhân khám tối đa trong ngày thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Xử lý trường hợp không thể ép kiểu thành công (giá trị không hợp lệ)
                // Ví dụ: Hiển thị thông báo lỗi cho người dùng
            }

        }

        //Hàm tìm kiếm thông tin bệnh nhân
        void _SearchCommand(QuanLiTiepDonView parameter)
        {
            ObservableCollection<BENHNHAN> temp = new ObservableCollection<BENHNHAN>();
            if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
            {
                string searchKeyword = parameter.txbSearch.Text.ToLower();
                temp = new ObservableCollection<BENHNHAN>(listBN.Where(s => s.HoTen.ToLower().Contains(searchKeyword)));
            }
            else
            {
                temp = new ObservableCollection<BENHNHAN>(listBN);
            }
            parameter.ListViewBN.ItemsSource = temp;
        }
        void _DeleteCommand(BENHNHAN selectedItem)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bệnh nhân này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    // Remove related HOADON records
                    var relatedHoadons = DataProvider.Ins.DB.HOADONs.Where(h => h.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.HOADONs.RemoveRange(relatedHoadons);
                    // Remove related CT_BCDT records
                    var relatedCTBCDTs = DataProvider.Ins.DB.CT_BCDT.Where(ct => ct.HOADON.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.CT_BCDT.RemoveRange(relatedCTBCDTs);
                    // Remove related PHIEUKHAMBENH records
                    var relatedPHIEUKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).ToList();
                    DataProvider.Ins.DB.PHIEUKHAMBENHs.RemoveRange(relatedPHIEUKBs);
                    var relatedMaPKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaBN == selectedItem.MaBN).Select(pkb => pkb.MaPKB).ToList();
                    var relatedCT_PKBs = DataProvider.Ins.DB.CT_PKB.Where(ctpkb => relatedMaPKBs.Contains(ctpkb.MaPKB)).ToList();
                    DataProvider.Ins.DB.CT_PKB.RemoveRange(relatedCT_PKBs);
                    // Remove the selected BENHNHAN
                    DataProvider.Ins.DB.BENHNHANs.Remove(selectedItem);

                    // Save changes to the database
                    DataProvider.Ins.DB.SaveChanges();

                    // Remove the item from the list
                    listBN.Remove(selectedItem);
                    UpdateSTT();
                    QuanLiTiepDonView quanLiTiepDonView = new QuanLiTiepDonView();
                    quanLiTiepDonView.txbSLBNDK.Text = listBN.Count.ToString();
                    quanLiTiepDonView.txbSLBNDK.FontSize = 25;
                }
            }
        }
        public void SetEditBNView(SuaBenhNhanView view)
        {
            EditBNView = view;
        }
        void _AddCommand(QuanLiTiepDonView parameter)
        {

            ThemBenhNhanView addBNView = new ThemBenhNhanView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            addBNView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addBNView.ShowDialog();

        }

        //hàm show màn hình edit
        void EditBN()
        {
            EditBNCommand = new ViewModelCommand(ShowWDEditBN);
        }
        private void ShowWDEditBN(object obj)
        {
            SuaBenhNhanView view = new SuaBenhNhanView((BENHNHAN)obj);
            view.ShowDialog();
            LoadData();
        }

        void LoadData()
        {
            listBN = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);

        }

    }
}
