using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.ViewModel.OtherViewModels;
namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    
    public class ThemThuocChoBenhNhanViewModel
    {
        public ICommand CancelCommand { get; set; }
        public ThemThuocChoBenhNhanViewModel()
        {
            CancelCommand = new RelayCommand<ThemThuocChoBenhNhanView>((p) => true, (p) => _CancelCommand(p));
        }
        void _CancelCommand(ThemThuocChoBenhNhanView paramater)
        {
            paramater.Close();
        }
    }
   
}
