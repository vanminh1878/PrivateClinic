using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;


namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class SuaQuiDinhThuocViewModel: BaseViewModel
    {
        public ICommand ExitCommand { get; set; }
        public SuaQuiDinhThuocViewModel()
        {
            ExitCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
        }
        private void _ExitCommand(ThayDoiQuiDinhThuocView p)
        {
            p.Close();
        }
    }
}
