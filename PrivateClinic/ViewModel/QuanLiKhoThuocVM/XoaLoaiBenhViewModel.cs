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

    public class XoaLoaiBenhViewModel : BaseViewModel
    {
        private ObservableCollection<PHIEUKHAMBENH> _benhnhan;
        public ObservableCollection<PHIEUKHAMBENH> benhnhan
        {
            get => _benhnhan;
            set { _benhnhan = value; OnPropertyChanged(nameof(benhnhan)); }

        }
        private ObservableCollection<LOAIBENH> _dvt;
        public ObservableCollection<LOAIBENH> dvt
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
        public XoaLoaiBenhViewModel()
        {
            LoadCommand = new RelayCommand<XoaLoaiBenhUS>((p) => { return p == null ? false : true; }, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<XoaLoaiBenhUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(XoaLoaiBenhUS p)
        {
            if (p.dvtcbx.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn loại bệnh cần xóa!.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa loại bệnh này ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (h == MessageBoxResult.Yes)
                {
                    benhnhan = new ObservableCollection<PHIEUKHAMBENH>(DataProvider.Ins.DB.PHIEUKHAMBENHs);
                    dvt = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
                    foreach (var lb in dvt)
                    {
                        if (lb.TenLoaiBenh == p.dvtcbx.Text)
                        {
                            // Kiểm tra xem loại bệnh này đã được sử dụng bởi bệnh nhân chưa
                            if (benhnhan.Any(bn => bn.MaLoaiBenh == lb.MaLoaiBenh))
                            {
                                MessageBox.Show("Không thể xóa loại bệnh này vì đã được sử dụng bởi một hoặc nhiều bệnh nhân.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                                return; // Thoát khỏi hàm nếu không thể xóa
                            }
                            else
                            {
                                // Xóa loại bệnh
                                DataProvider.Ins.DB.LOAIBENHs.Remove(lb);
                            }
                        }
                    }

                    DataProvider.Ins.DB.SaveChanges();
                    p.dvtcbx.SelectedIndex = -1;
                    MessageBox.Show("Xóa loại bệnh thành công!", "THÔNG BÁO");

                }
            }
            
        }
        private void _LoadCommand(XoaLoaiBenhUS p)
        {

            dvt = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            TenDVTs = dvt.Select(x => x.TenLoaiBenh).ToList();
        }
    }
}
