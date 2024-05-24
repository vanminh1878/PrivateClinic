using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;



namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{

    public class XoaDonViTinhViewModel:BaseViewModel
    {
        private ObservableCollection<DVT> _dvt;
        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<THUOC> _thuoc;
        public ObservableCollection<THUOC> thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(thuoc)); }

        }
        private List<string> _TenDVTs;
        public List<string> TenDVTs
        {
            get { return _TenDVTs; }
            set
            {
                _TenDVTs = value;
                OnPropertyChanged(nameof(TenDVTs));
            }
        }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public XoaDonViTinhViewModel()
        {
            LoadCommand = new RelayCommand<XoaDonViTinhUS>((p) => { return p == null ? false : true; }, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<XoaDonViTinhUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(XoaDonViTinhUS p)
        {
            if (p.dvtcbx.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn đơn vị tính cần xóa!.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa đơn vị tính này ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {

                    dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
                    thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                    foreach (var dv in dvt)
                    {
                        if (dv.TenDVT == p.dvtcbx.Text)
                        {
                            // Kiểm tra xem đơn vị tính này đã được sử dụng bởi thuốc chưa
                            if (thuoc.Any(t => t.MaDVT == dv.MaDVT))
                            {
                                MessageBox.Show("Không thể xóa đơn vị tính này vì đã được sử dụng bởi một hoặc nhiều thuốc.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                                return; // Thoát khỏi hàm nếu không thể xóa
                            }
                            else
                            {
                                // Xóa đơn vị tính
                                DataProvider.Ins.DB.DVTs.Remove(dv);
                                p.dvtcbx.SelectedIndex = -1;
                                MessageBox.Show("Xóa đơn vị tính thành công!", "THÔNG BÁO");
                            }
                        }
                    }
                    p.dvtcbx.SelectedIndex = -1;
                    MessageBox.Show("Xóa đơn vị tính thành công!", "THÔNG BÁO");

                }
            }
            
        }
        private void _LoadCommand(XoaDonViTinhUS p)
        {
            
            dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TenDVTs = dvt.Select(x => x.TenDVT).ToList();
        }
    }
}
