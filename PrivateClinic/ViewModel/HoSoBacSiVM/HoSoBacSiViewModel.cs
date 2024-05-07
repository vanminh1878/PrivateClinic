using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class HoSoBacSiViewModel : BaseViewModel
    {
        private object currentView;
        public object CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public ICommand SwitchViewCommand { get; private set; }
        public HoSoBacSiViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch(UserControlName)
            {
                case "Personal":
                    CurrentView = new ThongTinCaNhanView();
                    break;
                case "DoctorList":
                    CurrentView = new DanhSachBacSiView();
                    break;
                case "Password":
                    CurrentView = new DoiMatKhauView();
                    break;


            }
        }
    }
}
