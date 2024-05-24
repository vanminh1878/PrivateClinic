using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemDonViTinhViewModel:BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        
        public ICommand SaveCommand { get; set; }
        public ThemDonViTinhViewModel()
        {
            SaveCommand = new RelayCommand<ThemDonViTinhUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemDonViTinhUS p)
        {
            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm đơn vị tính mới ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {

                    DVT dVT = new DVT();
                    dVT.TenDVT = p.dvttbx.Text;
                    DataProvider.Ins.DB.DVTs.Add(dVT);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm đơn vị tính mới thành công!", "THÔNG BÁO");

                }
            }

           
        }
    }
}
