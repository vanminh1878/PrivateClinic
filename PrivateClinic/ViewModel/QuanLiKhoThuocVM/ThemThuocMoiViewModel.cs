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
    public class ThemThuocMoiViewModel: BaseViewModel
    {

        private ObservableCollection<DVT> _dvt;

        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<CACHDUNG> _cd;

        public ObservableCollection<CACHDUNG> cd
        {
            get => _cd;
            set { _cd = value; OnPropertyChanged(nameof(cd)); }
        }
        private ObservableCollection<LOAITHUOC> _lt;

        public ObservableCollection<LOAITHUOC> lt
        {
            get => _lt;
            set { _lt = value; OnPropertyChanged(nameof(lt)); }

        }
        private ObservableCollection<DVT> _donvitinh;

        public ObservableCollection<DVT> donvitinh
        {
            get => _donvitinh;
            set { _donvitinh = value; OnPropertyChanged(nameof(donvitinh)); }

        }
        private ObservableCollection<CACHDUNG> _cachdung;

        public ObservableCollection<CACHDUNG> cachdung
        {
            get => _cachdung;
            set { _cachdung = value; OnPropertyChanged(nameof(cachdung)); }

        }
        private ObservableCollection<LOAITHUOC> _loaithuoc;

        public ObservableCollection<LOAITHUOC> loaithuoc
        {
            get => _loaithuoc;
            set { _loaithuoc = value; OnPropertyChanged(nameof(loaithuoc)); }

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
        private List<string> _LoaiThuoc;
        public List<string> LoaiThuoc
        {
            get { return _LoaiThuoc; }
            set
            {
                _LoaiThuoc = value;
                OnPropertyChanged(nameof(LoaiThuoc));
            }
        }
        private ObservableCollection<THUOC> _thuoc;

        public ObservableCollection<THUOC> Thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(Thuoc)); }

        }
        private ObservableCollection<CT_PNT> _ctphieunhap;

        public ObservableCollection<CT_PNT> ctphieunhap
        {
            get => _ctphieunhap;
            set { _ctphieunhap = value; OnPropertyChanged(nameof(ctphieunhap)); }

        }
        private ObservableCollection<THAMSO> _thamso;

        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<PHIEUNHAPTHUOC> _phieunhap;

        public ObservableCollection<PHIEUNHAPTHUOC> phieunhap
        {
            get => _phieunhap;
            set { _phieunhap = value; OnPropertyChanged(nameof(phieunhap)); }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ThemThuocMoiViewModel()
        {
            LoadCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _SaveCommand(p));

        }
        private void _LoadCommand(ThemThuocMoiView p)
        {
            dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TenDVTs = dvt.Select(x => x.TenDVT).ToList();
            cd = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            CachDung = cd.Select(x => x.TenCachDung).ToList();
            lt = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            LoaiThuoc = lt.Select(x=>x.TenLoaiThuoc).ToList();


        }
        void _SaveCommand(ThemThuocMoiView paramater)
        {
            if (string.IsNullOrEmpty(paramater.SL.Text) || paramater.TenDVTcbx.SelectedItem == null || paramater.CachDungcbx.SelectedItem == null  || string.IsNullOrEmpty(paramater.TenThuoc.Text) || string.IsNullOrEmpty(paramater.SL.Text) || string.IsNullOrEmpty(paramater.DonGiaNhap.Text)||paramater.CachDungcbx.SelectedItem==null)
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn muốn lưu thông tin thuốc?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    donvitinh = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
                    cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
                    Thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                    phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);
                    ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
                    loaithuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
                    thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
                    
                    THUOC newthuoc = new THUOC();
                    {
                        newthuoc.MaThuoc= int.Parse(paramater.MaThuoc.Text);
                        newthuoc.TenThuoc = paramater.TenThuoc.Text;
                        newthuoc.DonGiaNhap = double.Parse(paramater.DonGiaNhap.Text);
                        newthuoc.SoLuong = int.Parse(paramater.SL.Text);
                        foreach(var ts in thamso)
                        {
                            if(ts.MaThamSo==3)
                            {
                                newthuoc.DonGiaBan = newthuoc.DonGiaNhap * ts.GiaTri;
                               
                            }
                        }


                        foreach (var dvt in donvitinh)
                        {
                            if(dvt.TenDVT== paramater.TenDVTcbx.SelectedItem.ToString())
                            {
                                newthuoc.MaDVT = dvt.MaDVT;
                            }
                        }
                        foreach (var lt in loaithuoc)
                        {
                            if (lt.TenLoaiThuoc == paramater.LoaiThuoccbx.SelectedItem.ToString())
                            {
                                newthuoc.MaLoaiThuoc = lt.MaLoaiThuoc;
                            }
                        }
                        foreach (var cd in cachdung)
                        {
                            if (cd.TenCachDung== paramater.CachDungcbx.SelectedItem.ToString())
                            {
                                newthuoc.MaCachDung = cd.MaCachDung;
                            }
                        }
                        int nextPN;
                        nextPN = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.Max(t => t.SoPhieuNhap) + 1;
                        PHIEUNHAPTHUOC pnt = new PHIEUNHAPTHUOC();
                        
                        pnt.SoPhieuNhap = nextPN;
                        pnt.NgayNhap = paramater.NgayNhap.SelectedDate;
                        //pnt.TongTien = paramater.SL.Text * ;
                        DataProvider.Ins.DB.PHIEUNHAPTHUOCs.Add(pnt);
                        //CT_PNT cT_PNT = new CT_PNT();
                        //int nextCTPN;
                        //nextCTPN = DataProvider.Ins.DB.CT_PNT.Max(t => t.SoPhieuNhap) + 1;
                        //cT_PNT.SoPhieuNhap= nextCTPN;
                        //if (cT_PNT.SoPhieuNhap== pnt.SoPhieuNhap)
                        //{
                        //    cT_PNT.MaThuoc = newthuoc.MaThuoc;
                        //    DataProvider.Ins.DB.CT_PNT.Add(cT_PNT);
                        //}
                                             
                     };
                    DataProvider.Ins.DB.THUOCs.Add(newthuoc);
                    
                    
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thuốc mới thành công!", "THÔNG BÁO");







                }
            }

        }
    }
}
