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

    public class ThemThuocCuViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _thuoc;
        public ObservableCollection<THUOC> thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(thuoc)); }

        }
        private List<string> _TenThuocs;
        public List<string> TenThuocs
        {
            get { return _TenThuocs; }
            set
            {
                _TenThuocs = value;
                OnPropertyChanged(nameof(TenThuocs));
            }
        }
        public ICommand LoadCommand { get; set; }
        public ThemThuocCuViewModel()
        {
            LoadCommand = new RelayCommand<ThemThuocCuView>((p) => true, (p) => _LoadCommand(p));
        }
        private void _LoadCommand(ThemThuocCuView p)
        {
            thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            TenThuocs = thuoc.Select(x => x.TenThuoc).ToList();


        }

    }
}
