using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Controls;


namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class QuanLiKhoThuocViewModel : BaseViewModel
    {

        public static QuanLiKhoThuocViewModel Instance { get; } = new QuanLiKhoThuocViewModel();

        public ICommand ExitCommand { get; set; }
       
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
        public ICommand SwitchViewCommand { get; set; }
        public QuanLiKhoThuocViewModel()
        {

          
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Quydinh":

                    CurrentView = new QuyDinhThuocView();
                    break;
                case "KhoThuoc":
                    CurrentView = new KhoThuocView();
                    break;
            }
        }

       



    }
}
