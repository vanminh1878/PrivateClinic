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
    
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditDvtCommand { get; set; }    
        public SuaQuiDinhThuocViewModel()
        {
            CancelCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            LoadCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _SaveCommand(p));
            EditDvtCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditDvtCommand(p));
        }
        private void _EditDvtCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaDonViTinhView a =  new SuaDonViTinhView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _LoadCommand(ThayDoiQuiDinhThuocView p)
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            CachDung = thamso.Where(x => x.MaThamSo == 6).Select(x => x.GiaTri.ToString()).ToList();
            Tilegia = thamso.Where(x => x.MaThamSo == 3).Select(x => x.GiaTri.ToString()).ToList();
            tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri.ToString()).ToList();
            loaibenh = thamso.Where(x => x.MaThamSo == 7).Select(x => x.GiaTri.ToString()).ToList();
            dvt = thamso.Where(x => x.MaThamSo == 5).Select(x => x.GiaTri.ToString()).ToList();

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
