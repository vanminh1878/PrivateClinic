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
        #region Các Property và Command
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
        #endregion
        //Hàm khởi tạo
        public QuanLiKhamBenhViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }

        //Hàm chuyển màn hình
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
