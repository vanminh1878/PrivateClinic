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
    public class ThemLoaiThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }

        public ICommand SaveCommand { get; set; }
        public ThemLoaiThuocViewModel()
        {
            SaveCommand = new RelayCommand<ThemLoaiThuocUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemLoaiThuocUS p)
        {
            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn loại thuốc mới ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {

                    LOAITHUOC dVT = new LOAITHUOC();
                    dVT.TenLoaiThuoc = p.dvttbx.Text;
                    DataProvider.Ins.DB.LOAITHUOCs.Add(dVT);
                    thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm loại thuốc mới thành công!", "THÔNG BÁO");

                }
            }
           
        }
    }
}
