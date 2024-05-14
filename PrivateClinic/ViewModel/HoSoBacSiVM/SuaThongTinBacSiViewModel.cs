using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class SuaThongTinBacSiViewModel : BaseViewModel
    {
        private SuaThongTinBacSiView _view;
        public SuaThongTinBacSiViewModel(SuaThongTinBacSiView view) 
        {
            this._view = view;
            CancelCommand = new ViewModelCommand(quit);

        }
        public ICommand CancelCommand { get; set; }
        private void quit(object obj)
        {
            _view.Close();
        }
    }
}
