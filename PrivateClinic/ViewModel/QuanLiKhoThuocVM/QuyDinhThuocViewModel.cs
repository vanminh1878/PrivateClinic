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
        private List<string> _CachDung;
        public List<string> CachDung
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
        private List<string> _dvt;
        public List<string> dvt
        {
            get { return _dvt; }
            set
            {
                _dvt = value;
                OnPropertyChanged(nameof(dvt));
            }
        }
        private List<string> _tienkham;
        public List<string> tienkham
        {
            get { return _tienkham; }
            set
            {
                _tienkham = value;
                OnPropertyChanged(nameof(tienkham));
            }
        }
        private List<string> _loaibenh;
        public List<string> loaibenh
        {
            get { return _loaibenh; }
            set
            {
                _loaibenh = value;
                OnPropertyChanged(nameof(loaibenh));
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
            CachDung = thamso.Where(x => x.MaThamSo == 6).Select(x => x.GiaTri.ToString()).ToList();
            Tilegia = thamso.Where(x => x.MaThamSo == 3).Select(x => x.GiaTri.ToString()).ToList();
            tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri.ToString()).ToList();
            loaibenh = thamso.Where(x => x.MaThamSo == 7).Select(x => x.GiaTri.ToString()).ToList();
            dvt = thamso.Where(x => x.MaThamSo == 5).Select(x => x.GiaTri.ToString()).ToList();

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
