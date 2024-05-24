using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemCachDungViewModel:BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }

        public ICommand SaveCommand { get; set; }
        public ThemCachDungViewModel()
        {
            SaveCommand = new RelayCommand<ThemCachDungUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemCachDungUS p)
        {

            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm cách dùng mới ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {


                    CACHDUNG cd = new CACHDUNG();
                    cd.TenCachDung = p.dvttbx.Text;
                    DataProvider.Ins.DB.CACHDUNGs.Add(cd);      
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm đơn cách dùng mới thành công!", "THÔNG BÁO");

                }
            }
            
        }
    }
}
