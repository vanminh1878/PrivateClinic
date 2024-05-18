using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinCaNhanViewModel : BaseViewModel
    {
        #region các property
        public string HoTen {  get; set; }
        public string MaBS { get; set; }
        public string formatMaBS { get; set; }

        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }

        private NGUOIDUNG user;
        public NGUOIDUNG User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        #endregion
        private ObservableCollection<BACSI> dsbs;
        public ObservableCollection<BACSI> DSBS
        {
            get => dsbs;
            set
            {
                dsbs = value;
                OnPropertyChanged(nameof(DSBS));
            }
        }
        public ThongTinCaNhanView _view;
        //Hàm khởi tạo
        public ThongTinCaNhanViewModel(ThongTinCaNhanView view) 
        {
            LoadData();
            this._view = view;
        }
        //Hàm load data lên listview
        void LoadData()
        {
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNG.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            MaBS = User.MaBS.ToString();
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSI);
            foreach (BACSI bac in DSBS)
            {
                if (bac.MaBS.ToString() == MaBS) 
                {
                    FormatMaBS();
                    formatMaBS = bac.formatMaBS;
                    HoTen = bac.HoTen;
                    GioiTinh = bac.GioiTinh;
                    SDT = bac.SDT;
                    Email = bac.Email;
                    NgaySinh = bac.NgaySinh;
                    NgayVL = bac.NgayVaoLam;
                    DiaChi = bac.DiaChi;
                    BangCap = bac.BangCap;
                    break;
                }
            }
           
            
                
        }

        // Hàm định dạng lại mã bác sĩ đưa lên view
        void FormatMaBS()
        {
            foreach (var item in DSBS)
            {
                if (item.MaBS < 10)
                {
                    item.formatMaBS = "BS00" + item.MaBS;
                }
                else if (item.MaBS < 100)
                {
                    item.formatMaBS = "BS0" + item.MaBS;
                }
                else
                    item.formatMaBS = "BS" + item.MaBS;
            }
        }

    }

    }

