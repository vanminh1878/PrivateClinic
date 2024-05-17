using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.Model;
using System.Collections.ObjectModel;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class ThemBenhNhanViewModel : BaseViewModel
    {

        public ICommand AddBN {  get; set; }
        public ICommand CancelCommand {  get; set; }
        public ThemBenhNhanViewModel()
        {
            AddBN = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _AddBN(p));
            CancelCommand = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _CancelCommand(p));
        }

        void _AddBN(ThemBenhNhanView paramater)
        {
            if (string.IsNullOrEmpty(paramater.HoTen.Text) || string.IsNullOrEmpty(paramater.GioiTinh.SelectedItem.ToString()) || string.IsNullOrEmpty(paramater.NgSinh.Text) || string.IsNullOrEmpty(paramater.MaBN.Text) || string.IsNullOrEmpty(paramater.DiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm bệnh nhân mới ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {


                    BENHNHAN a = new BENHNHAN();
                    a.HoTen = paramater.HoTen.Text;
                    a.GioiTinh = paramater.GioiTinh.Text;
                    a.DiaChi = paramater.DiaChi.Text;
                    a.NamSinh = (DateTime)paramater.NgSinh.SelectedDate;
                    a.MaBN=paramater.MaBN.Text;


                    MessageBox.Show("Thêm bệnh nhân mới thành công !", "THÔNG BÁO");
                    DataProvider.Ins.DB.BENHNHANs.Add(a);
                    DataProvider.Ins.DB.SaveChanges();
                    // Xóa thông tin các TextBox
                    paramater.HoTen.Clear();
                    paramater.GioiTinh.SelectedIndex = -1;
                    paramater.DiaChi.Clear();
                    paramater.MaBN.Clear();
                    paramater.NgSinh.SelectedDate = null;

                    QuanLiTiepDonView quanlitiepdonView = new QuanLiTiepDonView();
                    quanlitiepdonView.ListViewBN.ItemsSource = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);
                    quanlitiepdonView.ListViewBN.Items.Refresh();

                }
            }
        }
        void _CancelCommand(ThemBenhNhanView paramater)
        {       
            paramater.Close();
        }
    }
}
