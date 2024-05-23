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
    public class SuaQuiDinhThuocViewModel: BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
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
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditDvtCommand { get; set; }    
        public ICommand EditCachDungCommand { get; set; }    
        public ICommand EditSLBenhCommand { get; set; }    
        public ICommand EditSLThuocCommand { get; set; }    
        public SuaQuiDinhThuocViewModel()
        {
            CancelCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            LoadCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _SaveCommand(p));
            EditDvtCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditDvtCommand(p));
            EditCachDungCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditCachDungCommand(p));
            EditSLBenhCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditSLBenhCommand(p));
            EditSLThuocCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditSLThuocCommand(p));
        }
        private void _EditDvtCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaDonViTinhView a =  new SuaDonViTinhView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _EditCachDungCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaSoCachDungView a = new SuaSoCachDungView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _EditSLBenhCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaLoaiBenhView a = new SuaLoaiBenhView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _EditSLThuocCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaLoaiThuocView a = new SuaLoaiThuocView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _LoadCommand(ThayDoiQuiDinhThuocView p)
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            Loaibenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            Loaithuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            Dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            CachDung = cachdung.Count().ToString();
            Tilegia = thamso.Where(x => x.MaThamSo == 3).Select(x => x.GiaTri.ToString()).ToList();
            tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri.ToString()).ToList();
            loaibenh = Loaibenh.Count().ToString();
            loaithuoc = Loaithuoc.Count().ToString();
            dvt = Dvt.Count().ToString();

        }
        private void _ExitCommand(ThayDoiQuiDinhThuocView p)
        {
            p.Close();
        }
        private void _SaveCommand(ThayDoiQuiDinhThuocView p)
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            foreach(THAMSO t in thamso)
            {
                if(t.MaThamSo == 6)
                {
                    if (double.TryParse(p.cachdung.Text, out double giaTri))
                    {
                        t.GiaTri = giaTri;
                    }
                }
                else if (t.MaThamSo == 3)
                {
                    if (double.TryParse(p.Tilegia.Text, out double tile))
                    {
                        t.GiaTri = tile;
                    }
                }
                else if (t.MaThamSo == 2)
                {
                    if (double.TryParse(p.TienKham.Text, out double tienkham))
                    {
                        t.GiaTri = tienkham;
                    }
                }
                else if (t.MaThamSo == 7)
                {
                    if (double.TryParse(p.loaibenh.Text, out double loaibenh))
                    {
                        t.GiaTri = loaibenh;
                    }
                }
                else if (t.MaThamSo == 5)
                {
                    if (double.TryParse(p.Dvt.Text, out double dvt))
                    {
                        t.GiaTri = dvt;
                    }
                }
            }
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Cập nhật quy định mới thành công!", "THÔNG BÁO");
            p.Close();
        }
    }
}
