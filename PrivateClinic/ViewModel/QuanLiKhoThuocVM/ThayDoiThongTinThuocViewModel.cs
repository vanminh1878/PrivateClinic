﻿using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThayDoiThongTinThuocViewModel : BaseViewModel
    {
        public static ThayDoiThongTinThuocViewModel Instance { get; } = new ThayDoiThongTinThuocViewModel();

        public ThayDoiThongTinThuocView EditThuocView { get; set; }
        private ThayDoiThongTinThuocView EditThuocView2 { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetEditThuocView { get; set; }
        public ICommand LoadCommand { get; set; }
        private ObservableCollection<DVT> _dvt;

        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<string> _tenDVTs;
        public ObservableCollection<string> tenDVTs
        {
            get { return _tenDVTs; }
            set
            {
                _tenDVTs = value;
                OnPropertyChanged(nameof(tenDVTs));
            }
        }

        public ThayDoiThongTinThuocViewModel()
        {
            CancelCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, p => _CancelCommand(p));
            SaveCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _SaveCommand(p));
            LoadCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _LoadCommand(p));
            SetEditThuocView = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _SetEditThuocView(p));
        }

        public void _SetEditThuocView(ThayDoiThongTinThuocView view)
        {
            if (EditThuocView == null)
            {
                EditThuocView = view;
                EditThuocView2 = EditThuocView;
            }
        }
        void  _LoadCommand(ThayDoiThongTinThuocView p)
        {
            // Lấy danh sách các TenDVT từ dvt
            var tenDVTs = dvt.Select(x => x.TenDVT);

            // Gán danh sách TenDVT vào các ComboBoxItem của p.TenDVT
            p.TenDVT.ItemsSource = tenDVTs;
        }
        void _SaveCommand(ThayDoiThongTinThuocView paramater)
        {
            var p = new ThayDoiThongTinThuocView();
            p.TenThuoc.Text = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenThuoc.Text;
            p.NgayNhap.SelectedDate = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.NgayNhap.SelectedDate;
            p.DonGiaNhap.Text = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.DonGiaNhap.Text;
            p.TenDVT.Text = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.TenDVT.Text;
            p.SoLuong.Text = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.SoLuong.Text;

            if (string.IsNullOrEmpty(p.TenThuoc.Text) || p.NgayNhap.SelectedDate == null || string.IsNullOrEmpty(p.DonGiaNhap.Text) || string.IsNullOrEmpty(p.TenDVT.SelectedItem.ToString()) || string.IsNullOrEmpty(p.SoLuong.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn muốn lưu thông tin thuốc?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string maThuoc = ThayDoiThongTinThuocViewModel.Instance.EditThuocView.MaThuoc.Text;
                    THUOC thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault();
                    if (thuoc != null)
                    {
                        thuoc.TenThuoc = p.TenThuoc.Text;
                        //thuoc.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;
                        thuoc.DonGiaNhap = double.Parse(p.DonGiaNhap.Text);
                        //thuoc.TenDVT = p.TenDVT.Text;
                        //thuoc.SoLuong = int.Parse(p.SoLuong.Text);

                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin thuốc thành công!", "THÔNG BÁO");


                        QuanLiKhoThuocView quanLiThuocView = new QuanLiKhoThuocView();
                        quanLiThuocView.MedicineListView.ItemsSource = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                        quanLiThuocView.MedicineListView.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thuốc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        void _CancelCommand(ThayDoiThongTinThuocView paramater)
        {
            paramater.Close();
        }
    }
}
