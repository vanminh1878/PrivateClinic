using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class SuaThongTinDonThuocViewModel:BaseViewModel
    {
        public static SuaThongTinDonThuocViewModel Instance { get; } = new SuaThongTinDonThuocViewModel();
        public SuaThongTinDonThuocView EditMedView { get; set; }
        public ICommand CancelCommand { get; set; }
        public SuaThongTinDonThuocViewModel()
        {
            CancelCommand = new RelayCommand<SuaThongTinDonThuocView>((p) => true, (p) => _CancelCommand(p));
        }
        void _CancelCommand(SuaThongTinDonThuocView paramater)
        {
            paramater.Close();
        }
    }
}
