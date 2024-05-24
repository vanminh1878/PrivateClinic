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

    public class XoaLoaiThuocViewModel : BaseViewModel
    {
        private ObservableCollection<LOAITHUOC> _dvt;
        public ObservableCollection<LOAITHUOC> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

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
        public XoaLoaiThuocViewModel()
        {
            LoadCommand = new RelayCommand<XoaLoaiThuocUS>((p) => { return p == null ? false : true; }, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<XoaLoaiThuocUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(XoaLoaiThuocUS p)
        {
            if (p.dvtcbx.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn loại thuốc cần xóa!.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa loại thuốc này ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {

                    thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                    foreach (var lt in dvt)
                    {
                        if (lt.TenLoaiThuoc == p.dvtcbx.Text)
                        {
                            // Kiểm tra xem loại thuốc này đã được sử dụng bởi đơn thuốc chưa
                            if (thuoc.Any(dt => dt.MaLoaiThuoc == lt.MaLoaiThuoc))
                            {
                                MessageBox.Show("Không thể xóa loại thuốc này vì đã được sử dụng bởi một hoặc nhiều đơn thuốc.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                                return; // Thoát khỏi hàm nếu không thể xóa
                            }
                            else
                            {
                                // Xóa loại thuốc
                                DataProvider.Ins.DB.LOAITHUOCs.Remove(lt);
                                p.dvtcbx.SelectedIndex = -1;
                                MessageBox.Show("Xóa loại thuốc thành công!", "THÔNG BÁO");
                            }
                           
                        }
                    }                    

                }
            }
            
        }
        private void _LoadCommand(XoaLoaiThuocUS p)
        {

            dvt = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            TenDVTs = dvt.Select(x => x.TenLoaiThuoc).ToList();
        }
    }
}
