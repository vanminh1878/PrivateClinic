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
    public class ThemBacSiViewModel
    {
        //Hàm khỏi tạo
        public ThemBacSiViewModel(ThemBacSiView wd) 
        {
            this.view = wd;
            quitCommand = new ViewModelCommand(quit);
        }
        // Command thoát khỏi màn hình thêm bác sĩ
        public ICommand quitCommand { get; set; }

        private ThemBacSiView view;

        private void quit(object obj)
        {
            view.Close();
        }
    }
}
