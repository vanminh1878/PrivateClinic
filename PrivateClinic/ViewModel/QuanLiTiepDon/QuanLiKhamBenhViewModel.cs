using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class QuanLiKhamBenhViewModel : BaseViewModel
    {
        private UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public ICommand KhamBNCommand { get; set; }
        public ICommand SwitchViewCommand { get; set; }


        public QuanLiKhamBenhViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            
        }

        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "BNdangkham":
                    CurrentView = new BenhNhanDangKhamView();
                    break;
                case "DSBenhNhan":
                    CurrentView = new QuanLiTiepDonView();
                    break;
            }
        }
        private void KhamBNCommandExecute(object parameter)
        {
            CurrentView = new BenhNhanDangKhamView();
        }
    }
}