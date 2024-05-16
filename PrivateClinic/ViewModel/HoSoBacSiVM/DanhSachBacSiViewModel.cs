﻿using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class DanhSachBacSiViewModel: BaseViewModel
    {
        private ObservableCollection<BACSI> dsbs;
        public ObservableCollection<BACSI> DSBS
        {
            get => dsbs;
            set
            {
                dsbs = value;
                OnPropertyChanged(nameof(DSBS));
            }
        }
        public ICommand ShowWDAddDoctor { get; set; }
        public ICommand EditDoctorCommand { get; set; }
        public ICommand DeleteDoctorCommand { get; set; }

        //Hàm khởi tạo
        public DanhSachBacSiViewModel()
        {
            LoadData();
            AddDoctor();
            EditDoctor();
            DeleteDoctorCommand = new RelayCommand<BACSI>((p) => p != null, DeleteBacSi);

        }
        //Chức năng tìm kiếm

        private ObservableCollection<BACSI> filterDSBS;
        public ObservableCollection<BACSI> FilterDSBS 
        { 
            get => filterDSBS; 
            set
            {
                if(filterDSBS != value)
                {
                    filterDSBS = value;
                    OnPropertyChanged(nameof(FilterDSBS));
                }
            }
        }

        private string searchText;
        public string SearchText
        { get => searchText;
            set
        {
                if(searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterDSBacSi();
                }
            }
        }
        void SearchBS()
        {
            FilterDSBS = new ObservableCollection<BACSI>(DSBS);//ban đầu thì không cần lọc
        }
        private void FilterDSBacSi()
        {
            // Cập nhật FilteredDSBS dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSBS = new ObservableCollection<BACSI>(DSBS);
            }
            else
            {
                FilterDSBS = new ObservableCollection<BACSI>(
                    DSBS.Where(s => s.HoTen.ToLower().Contains(SearchText.ToLower())));
            }
        }
             
        //Chức năng hiển thị danh sách bác sĩ
        void LoadData()
        {
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            SearchBS();
        }

        // Hàm chức năng thêm mới 1 bác sĩ
        private void ShowWDDoctor(object obj)
        {
            ThemBacSiView view = new ThemBacSiView();
            LoadData();
            view.ShowDialog();
        }

        void AddDoctor()
        {
            ShowWDAddDoctor = new ViewModelCommand(ShowWDDoctor);
        }

        // Hàm chức năng sửa thông tin cho 1 bác sĩ
        void EditDoctor()
        {
            EditDoctorCommand = new ViewModelCommand(ShowWDEditDoctor);
        }
        private void ShowWDEditDoctor(object obj)
        {
            SuaThongTinBacSiView view = new SuaThongTinBacSiView((BACSI) obj);
            view.ShowDialog();
            LoadData();
        }

        //hàm chức năng xóa thông tin 1 bác sĩ
        void DeleteBacSi(BACSI selectedItem)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa bệnh nhân này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes) 
            {
                if (selectedItem != null) 
                {
                    //Xóa tài khoàn trước
                    var taikhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.NGUOIDUNGs.RemoveRange(taikhoan);
                    //Cập nhật mã bác sĩ trong các phiếu khám bệnh bằng NULL
                    var phieukhambenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    foreach (var h in phieukhambenh) 
                        h.MaBS = null;
                    //Xóa bác sĩ
                    DataProvider.Ins.DB.BACSIs.Remove(selectedItem);

                    //Lưu xuống CSDL
                    DataProvider.Ins.DB.SaveChanges();

                    //Cập nhật listview
                    FilterDSBS.Remove(selectedItem);
                    MessageBox.Show("Xóa thành công", "Thông báo");
                }
            }
        }
    } 
}
