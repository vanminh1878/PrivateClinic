using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows;
using PrivateClinic.Model;
using System.Collections.ObjectModel;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    
    public class QuyDinhThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<CACHDUNG> _cachdung;
        public ObservableCollection<CACHDUNG> cachdung
        {
            get => _cachdung;
            set { _cachdung = value; OnPropertyChanged(nameof(cachdung)); }

        }
        private ObservableCollection<LOAIBENH> _Loaibenh;
        public ObservableCollection<LOAIBENH> Loaibenh
        {
            get => _Loaibenh;
            set { _Loaibenh = value; OnPropertyChanged(nameof(Loaibenh)); }

        }
        private ObservableCollection<LOAITHUOC> _Loaithuoc;
        public ObservableCollection<LOAITHUOC> Loaithuoc
        {
            get => _Loaithuoc;
            set { _Loaithuoc = value; OnPropertyChanged(nameof(Loaithuoc)); }

        }
        private ObservableCollection<DVT> _Dvt;
        public ObservableCollection<DVT> Dvt
        {
            get => _Dvt;
            set { _Dvt = value; OnPropertyChanged(nameof(Dvt)); }

        }
        private string _CachDung;
        public string CachDung
        {
            get { return _CachDung; }
            set
            {
                _CachDung = value;
                OnPropertyChanged(nameof(CachDung));
            }
        }
        private List<string> _Tilegia;
        public List<string> Tilegia
        {
            get { return _Tilegia; }
            set
            {
                _Tilegia = value;
                OnPropertyChanged(nameof(Tilegia));
            }
        }
        private string _dvt;
        public string dvt
        {
            get { return _dvt; }
            set
            {
                _dvt = value;
                OnPropertyChanged(nameof(dvt));
            }
        }
        private List<double> _tienkham;
        public List<double> tienkham
        {
            get { return _tienkham; }
            set
            {
                _tienkham = value;
                OnPropertyChanged(nameof(tienkham));
            }
        }
        private string _loaibenh;
        public string loaibenh
        {
            get { return _loaibenh; }
            set
            {
                _loaibenh = value;
                OnPropertyChanged(nameof(loaibenh));
            }
        }
        private string _loaithuoc;
        public string loaithuoc
        {
            get { return _loaithuoc; }
            set
            {
                _loaithuoc = value;
                OnPropertyChanged(nameof(loaithuoc));
            }
        }
        public ICommand EditQDCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public QuyDinhThuocViewModel() 
        {
            EditQDCommand = new RelayCommand<QuyDinhThuocView>((p) => { return p == null ? false : true; }, (p) => _EditQDCommand(p));
            LoadCommand = new RelayCommand<QuyDinhThuocView>((p) => true, (p) => _LoadCommand(p));

        }
        private void _LoadCommand(QuyDinhThuocView p)
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            Loaibenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            Loaithuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            Dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            CachDung = cachdung.Count().ToString();
            Tilegia = thamso.Where(x => x.MaThamSo == 3).Select(x => x.GiaTri.ToString()).ToList();
            //tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri.ToString()).ToList();
            tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri).ToList();
            loaibenh = Loaibenh.Count().ToString();
            loaithuoc = Loaithuoc.Count().ToString();
            dvt = Dvt.Count().ToString();

        }
        private void _EditQDCommand(QuyDinhThuocView p)
        {
            ThayDoiQuiDinhThuocView a = new ThayDoiQuiDinhThuocView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }

    }
}
