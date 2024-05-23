﻿using PrivateClinic.Model;
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
        private ObservableCollection<BENHNHAN> filterDSBN;
        public ObservableCollection<BENHNHAN> FilterDSBN
        {
            get => filterDSBN;
            set
            {
                if (filterDSBN != value)
                {
                    filterDSBN = value;
                    OnPropertyChanged(nameof(FilterDSBN));

                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterDSBenhNhan();
                }
            }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }    
        public ICommand LoadSLBNCommand { get; set; }    
        public ICommand EditSLBNCommand { get; set; }    
        public ICommand EditUpSLBNCommand { get; set; }    
        public ICommand EditDownSLBNCommand { get; set; }    
        public ICommand SaveSLBNCommand { get; set; }    
        public ICommand EditBNCommand { get; set; }

        public ICommand selectedItemBenhNhan { get; set; }
        private int soLuongBenhNhanDaTiepDon;
        public int SoLuongBenhNhanDaTiepDon
        {
            get => soLuongBenhNhanDaTiepDon;
            set
            {
                soLuongBenhNhanDaTiepDon = value;
                OnPropertyChanged();
            }
        }


        public QuanLiTiepDonViewModel()
        {
            LoadData();
            DeleteCommand = new RelayCommand<BENHNHAN>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            AddCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            LoadSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _LoadSLBNCommand(p));
            EditBN();
            if (Const.PQ.MaNhom == "NHOM1")
            {
                EditSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditSLBNCommand(p));
                EditUpSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditUpSLBNCommand(p));
                EditDownSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _EditDownSLBNCommand(p));
                SaveSLBNCommand = new RelayCommand<QuanLiTiepDonView>((p) => { return p == null ? false : true; }, (p) => _SaveSLBNCommand(p));
            }
            
        }

   
       
        void _EditSLBNCommand(QuanLiTiepDonView parameter)
        {
          
            parameter.txbMaxBN.IsEnabled = true;
            parameter.btnUp.IsEnabled = true;
            parameter.btnDown.IsEnabled = true;
            parameter.btnSave.Visibility = Visibility.Visible;
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
                    if (newValue < SoLuongBenhNhanDaTiepDon)
                    {
                        MessageBox.Show("Số lượng bệnh nhân khám tối đa trong ngày không hợp lệ","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                    else
                    {
                        t.GiaTri = newValue;
                        DataProvider.Ins.DB.SaveChanges();
                        parameter.txbMaxBN.IsEnabled = false;
                        parameter.btnSave.IsEnabled = false;
                        MessageBox.Show("Đã lưu số bệnh nhân khám tối đa trong ngày thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        parameter.btnSave.Visibility = Visibility.Hidden;

                    }

                }
            }
            else
            {
                MessageBox.Show("Giá trị nhập lỗi","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

        //Hàm tìm kiếm thông tin bệnh nhân
        void SearchBN()
        {
            FilterDSBN = new ObservableCollection<BENHNHAN>(listBN);//ban đầu thì không cần lọc
        }
        private void FilterDSBenhNhan()
        {
            // Cập nhật FilteredDSBS dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSBN = new ObservableCollection<BENHNHAN>(listBN);
            }
            else
            {
                FilterDSBN = new ObservableCollection<BENHNHAN>(
                    listBN.Where(s => s.HoTen.ToLower().Contains(SearchText.ToLower())));
            }
        }

        void _DeleteCommand(BENHNHAN selectedItem)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bệnh nhân này ra khỏi danh sách tiếp đón không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    selectedItem.TrangThai = true;
                    // Save changes to the database
                    DataProvider.Ins.DB.SaveChanges();
                    LoadData();
                    MessageBox.Show("Đã xóa ra khỏi danh sách tiếp đón", "Thông báo", MessageBoxButton.OK);
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
            LoadData();

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
            var benhnhanList = DataProvider.Ins.DB.BENHNHANs.Where(p => p.TrangThai == false).ToList();
            listBN = new ObservableCollection<BENHNHAN>(benhnhanList);
            SearchBN();
            int dem = listBN.Count;
            int soluong = 0;
            ObservableCollection<PHIEUKHAMBENH> listpkb = new ObservableCollection<PHIEUKHAMBENH>(DataProvider.Ins.DB.PHIEUKHAMBENHs);
            foreach (var pkb in listpkb)
            {
                if (pkb.NgayKham.Date == DateTime.UtcNow.Date)
                {
                    soluong++;
                }
            }
            SoLuongBenhNhanDaTiepDon = dem+soluong;
            
        }


    }
}
