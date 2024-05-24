﻿using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using PrivateClinic.Model;
using System.Windows;
using PrivateClinic.View.MessageBox;
namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    
    public class ThemThuocChoBenhNhanViewModel:BaseViewModel
    {
        #region Các property và Command
        private ObservableCollection<ThuocDTO> listthuocDTO;
        public ObservableCollection<ThuocDTO> ListThuocDTO
        {
            get => listthuocDTO;
            set
            {
                listthuocDTO = value;
                OnPropertyChanged(nameof(ListThuocDTO));
                SoLuongThuocDaChon = ListThuocDTO.Count;
            }
        }
        private ObservableCollection<THUOC> listthuoc;
        public ObservableCollection <THUOC> ListThuoc
        {
            get { return listthuoc; }
            set
            {
                listthuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }

        private THUOC selectedThuoc;
        public THUOC SelectedThuoc
        {
            get => selectedThuoc;
            set
            {
                if (selectedThuoc != value)
                {
                    selectedThuoc = value;
                    OnPropertyChanged(nameof(SelectedThuoc));
                    XacDinhTenDonVi();
                    XacDinhMaThuoc();
                }
            }
        }

        private ObservableCollection<CACHDUNG> listcachdung;
        public ObservableCollection<CACHDUNG> ListCachDung
        {
            get => listcachdung;
            set
            {
                listcachdung = value;
                OnPropertyChanged(nameof(ListCachDung));
            }
        }

        private CACHDUNG selectedCachDung;
        public CACHDUNG SelectedCachDung
        {
            get => selectedCachDung;
            set
            {
                selectedCachDung = value;
                OnPropertyChanged(nameof (SelectedCachDung));
            }
        }

        private ObservableCollection<DVT> listDVT;
        public ObservableCollection<DVT> ListDVT
        {
            get => listDVT;
            set
            {
                listDVT = value;
                OnPropertyChanged(nameof(ListDVT));
            }
        }
        private string donvi;
        public string DonVi
        {
            get => donvi;
            set
            {
                donvi = value;
                OnPropertyChanged(nameof(DonVi));
            }
        }

        private string mathuoc;
        public string MaThuoc
        {
            get => mathuoc;
            set
            {
                mathuoc = value;
                OnPropertyChanged(nameof(MaThuoc));
            }
        }

        private string soluong;
        public string SoLuong
        {
            get => soluong;
            set
            {
                soluong = value;
                OnPropertyChanged(nameof(SoLuong));
            }
        }

        private int soLuongThuocDaChon;
        public int SoLuongThuocDaChon
        {
            get => soLuongThuocDaChon;
            set
            {
                if(soLuongThuocDaChon != value)
                {
                    soLuongThuocDaChon = value;
                    OnPropertyChanged(nameof(SoLuongThuocDaChon));
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        private ThemThuocChoBenhNhanView _view;
        #endregion
        public ThemThuocChoBenhNhanViewModel(ThemThuocChoBenhNhanView view)
        {
            this._view = view;
            ListThuocDTO = new ObservableCollection<ThuocDTO>();
            ListThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            ListCachDung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            ListDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            CancelCommand = new RelayCommand<ThemThuocChoBenhNhanView>((p) => true, (p) => _CancelCommand(p));
            AddCommand = new ViewModelCommand(AcceptAdd);
            DeleteCommand = new RelayCommand<ThuocDTO>((p) => p != null, DeleteAccept);
            SaveCommand = new ViewModelCommand(Save);
        }
        void _CancelCommand(object paramater)
        {
            _view.Close();
        }

        private void XacDinhTenDonVi()
        {
            if (SelectedThuoc != null)
            {
                var thuoc = ListDVT.FirstOrDefault(dvt => dvt.MaDVT == SelectedThuoc.MaDVT);
                DonVi = thuoc != null ? thuoc.TenDVT : string.Empty;
            }
            else
            {
                DonVi = "";
            }
        }

        private void XacDinhMaThuoc()
        {
            if (SelectedThuoc != null) 
            {
                var thuoc = ListThuoc.FirstOrDefault(ma => ma.MaThuoc == selectedThuoc.MaThuoc);
                MaThuoc = thuoc.MaThuoc.ToString();
            }
            else
            {
                MaThuoc = "";
            }
        }
        int stt = 0;

        //Chức năng thêm
        private void AcceptAdd(object obj)
        {
            ObservableCollection<THUOC> list = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            var thuoc = list.FirstOrDefault(x => x.MaThuoc == SelectedThuoc.MaThuoc);
            if (SelectedThuoc == null || SelectedCachDung == null || string.IsNullOrEmpty(SoLuong)) 
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
                mb.ShowDialog();
            }
            else if (!SoLuong.All(char.IsDigit))
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Số lượng chỉ chứa số");
                mb.ShowDialog();
            }
            else if (int.Parse(SoLuong) > SelectedThuoc.SoLuong)
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Số lượng thuốc không đủ");
                mb.ShowDialog();
            }
            else
            {
                ThuocDTO thuocDTO = new ThuocDTO();
                stt++;
                thuocDTO.STT = stt;
                thuocDTO.MaThuoc = MaThuoc;
                thuocDTO.TenThuoc = selectedThuoc.TenThuoc;
                thuocDTO.SL = int.Parse(SoLuong);
                SelectedThuoc.SoLuong = SelectedThuoc.SoLuong - int.Parse(SoLuong);
                thuocDTO.CachDung = SelectedCachDung.TenCachDung;
                thuocDTO.DVT = DonVi;
                ListThuocDTO.Add(thuocDTO);
                SoLuongThuocDaChon = ListThuocDTO.Count();
                SelectedThuoc = null;
                SoLuong = "";
                SelectedCachDung = null;
                DonVi = "";
                MaThuoc = "";
            }
        }
        //Chức năng xóa
        private void DeleteAccept (ThuocDTO selecteditem)
        {
            YesNoMessageBox h = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa thuốc này không");
            h.ShowDialog();
            if (h.DialogResult == true)
            {
                if (selecteditem != null)
                {
                    ListThuocDTO.Remove(selecteditem);
                }
            }
        }
        //Chức năng lưu
        private void Save(object obj)
        {
            if (ListThuocDTO == null || !ListThuocDTO.Any())
            {
                OkMessageBox mbs = new OkMessageBox("Thông báo", "Không có thuốc nào để lưu");
                mbs.ShowDialog();
                return;
            }
            Const.ListThuoc = new ObservableCollection<ThuocDTO>();
            // Assuming benhnhanvm.ListThuoc is of type List<ThuocDTO> or ObservableCollection<ThuocDTO>
            foreach (var thuocDTO in ListThuocDTO)
            {
                Const.ListThuoc.Add(thuocDTO);
            }

            OkMessageBox mb = new OkMessageBox("Thông báo", "Đã thêm thuốc cho bệnh nhân");
            mb.ShowDialog();
            _view.Close();
        }






    }

}

    