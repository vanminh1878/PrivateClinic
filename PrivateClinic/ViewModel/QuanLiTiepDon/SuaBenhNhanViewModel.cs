using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class SuaBenhNhanViewModel : BaseViewModel
    {
        public static SuaBenhNhanViewModel Instance { get; } = new SuaBenhNhanViewModel();

        public SuaBenhNhanView EditBNView { get; set; }
        private SuaBenhNhanView EditBNView2 { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetEditBNView { get; set; }
        public SuaBenhNhanViewModel()
        {
            CancelCommand = new RelayCommand<SuaBenhNhanView>((p) => true, p => _CancelCommand(p));
            SaveCommand = new RelayCommand<SuaBenhNhanView>((p) => true, (p) => _SaveCommand(p));
            SetEditBNView = new RelayCommand<SuaBenhNhanView>((p) => true, (p) => _SetEditBNView(p));


        }

        public void _SetEditBNView(SuaBenhNhanView view)
        {
            if (EditBNView == null)
            {
                EditBNView = view;
                EditBNView2 = EditBNView;
            }

        }

        void _SaveCommand(SuaBenhNhanView paramater)
        {
            SuaBenhNhanView p = new SuaBenhNhanView();
            p.HoTen.Text = SuaBenhNhanViewModel.Instance.EditBNView.HoTen.Text;
            p.NgSinh.Text = SuaBenhNhanViewModel.Instance.EditBNView.NgSinh.Text;
            p.GioiTinh.Text = SuaBenhNhanViewModel.Instance.EditBNView.GioiTinh.Text;
            p.DiaChi.Text = SuaBenhNhanViewModel.Instance.EditBNView.DiaChi.Text;
            p.MaBN.Text = SuaBenhNhanViewModel.Instance.EditBNView.MaBN.Text;

            if (string.IsNullOrEmpty(p.HoTen.Text) || string.IsNullOrEmpty(p.GioiTinh.SelectedItem.ToString()) || string.IsNullOrEmpty(p.NgSinh.Text) || string.IsNullOrEmpty(p.DiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn lưu thông tin bệnh nhân ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    string maBN = p.MaBN.Text;
                    BENHNHAN a = DataProvider.Ins.DB.BENHNHANs.FirstOrDefault(bn => bn.MaBN.ToString() == maBN);
                    a.HoTen = p.HoTen.Text;
                    a.GioiTinh = p.GioiTinh.Text;
                    a.DiaChi = p.DiaChi.Text;
                    a.NamSinh = (DateTime)p.NgSinh.SelectedDate;
                    MessageBox.Show("Cập nhật thông tin bệnh nhân thành công!", "THÔNG BÁO");
                    DataProvider.Ins.DB.SaveChanges();
                    QuanLiTiepDonView quanlitiepdonView = new QuanLiTiepDonView();
                    quanlitiepdonView.ListViewBN.ItemsSource = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);
                    quanlitiepdonView.ListViewBN.Items.Refresh();
                }
            }
        }

        void _CancelCommand(SuaBenhNhanView paramater)
        {
            paramater.Close();
        }
    }
}