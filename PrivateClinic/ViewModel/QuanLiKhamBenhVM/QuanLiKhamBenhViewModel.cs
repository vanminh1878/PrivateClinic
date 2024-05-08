using PrivateClinic.View.QuanLiKhamBenh;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhamBenhVM
{
    public class QuanLiKhamBenhViewModel : BaseViewModel
    {
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand SwitchViewCommand { get; set; }

        public QuanLiKhamBenhViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }

        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "PatientList":
                    CurrentView = new BenhNhanDaKhamView();
                    break;
            }
        }
    }
}
