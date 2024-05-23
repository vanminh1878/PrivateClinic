using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.Model;
using System.Collections.ObjectModel;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class ThemBenhNhanViewModel : BaseViewModel
    {
        //Lấy danh sách bệnh nhân 
        private ObservableCollection<BENHNHAN> listBenhNhan; 
        public ObservableCollection<BENHNHAN> ListBenhNhan
        {
            get => listBenhNhan;
            set
            {
                listBenhNhan = value;
                OnPropertyChanged(nameof(ListBenhNhan));
            }
        }

        private BENHNHAN selectedBenhNhan;
        public BENHNHAN SelectedBenhNhan
        {
            get => selectedBenhNhan;
            set
            {
                if (selectedBenhNhan != value)
                {
                    selectedBenhNhan = value;
                    OnPropertyChanged(nameof(SelectedBenhNhan));
                    XacDinhThongTinChoBenhNhanCu();
                }
                
            }
        }

        //Địa chỉ
        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                diachi = value;
                OnPropertyChanged(nameof (DiaChi));
            }
        }
        //Giới tính
        private string gioitinh;
        public string GioiTinh
        {
            get => gioitinh;
            set
            {
                gioitinh = value;
                OnPropertyChanged(nameof(GioiTinh));
            }
        }
        //Ngày sinh
        private DateTime? ngaysinh;
        public DateTime? NgaySinh
        {
            get => ngaysinh;
            set
            {
                ngaysinh = value;
                OnPropertyChanged(nameof(NgaySinh));
            }
        }

        public ICommand AddBN {  get; set; }
        public ICommand CancelCommand {  get; set; }
        public ThemBenhNhanViewModel()
        {
            ListBenhNhan = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);
            AddBN = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _AddBN(p));
            CancelCommand = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _CancelCommand(p));
        }

        void _AddBN(ThemBenhNhanView paramater)
        {
            if (string.IsNullOrEmpty(paramater.HoTen.Text) || string.IsNullOrEmpty(paramater.GioiTinh.SelectedItem.ToString()) || string.IsNullOrEmpty(paramater.NgSinh.Text) || string.IsNullOrEmpty(paramater.DiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var benhnhan = ListBenhNhan.FirstOrDefault(bn => bn.MaBN == SelectedBenhNhan?.MaBN);
                if (benhnhan != null)
                {
                    MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm bệnh nhân cũ vào danh sách tiếp đón ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (h == MessageBoxResult.Yes)
                    {
                        benhnhan.TrangThai = false;
                        MessageBox.Show("Đã thêm bệnh nhân vào danh sách tiếp đón!", "THÔNG BÁO");
                    }
                        
                }
                else
                {
                    MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm bệnh nhân mới vào danh sách tiếp đón ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (h == MessageBoxResult.Yes)
                    {
                        BENHNHAN a = new BENHNHAN();
                        a.HoTen = paramater.HoTen.Text;
                        a.GioiTinh = paramater.GioiTinh.Text;
                        a.DiaChi = paramater.DiaChi.Text;
                        a.NamSinh = (DateTime)paramater.NgSinh.SelectedDate;
                        a.TrangThai = false;
                        MessageBox.Show("Đã thêm bệnh nhân vào danh sách tiếp đón!", "THÔNG BÁO");
                        DataProvider.Ins.DB.BENHNHANs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        // Xóa thông tin các TextBox
                        paramater.HoTen.Text = string.Empty;
                        paramater.GioiTinh.SelectedIndex = -1;
                        paramater.DiaChi.Clear();
                        paramater.NgSinh.SelectedDate = null;
                    }
                }
            }
        }
        void _CancelCommand(ThemBenhNhanView paramater)
        {       
            paramater.Close();
        }

        private void XacDinhThongTinChoBenhNhanCu()
        {
            if (SelectedBenhNhan != null)
            {
                var benhnhan = ListBenhNhan.FirstOrDefault(bn => bn.MaBN == SelectedBenhNhan.MaBN);
                if (benhnhan != null) 
                {
                    DiaChi = benhnhan.DiaChi;
                    NgaySinh = benhnhan.NamSinh;
                    GioiTinh = benhnhan.GioiTinh;
                }
                else
                {
                    DiaChi = "";
                    NgaySinh = null;
                    GioiTinh = "";
                }
            }
            else
            {
                DiaChi = "";
                NgaySinh = null;
                GioiTinh = "";
            }
        }
    }
}
