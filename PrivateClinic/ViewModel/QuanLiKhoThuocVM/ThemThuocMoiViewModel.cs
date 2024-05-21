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

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemThuocMoiViewModel: BaseViewModel
    {
        private ObservableCollection<DVT> _dvt;

        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

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
        public ThemThuocMoiViewModel()
        {
            LoadCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _LoadCommand(p));

        }
        private void _LoadCommand(ThemThuocMoiView p)
        {
            dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TenDVTs = dvt.Select(x => x.TenDVT).ToList();


        }
    }
}
