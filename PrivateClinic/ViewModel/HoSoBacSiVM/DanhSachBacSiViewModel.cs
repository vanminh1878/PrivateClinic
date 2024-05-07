using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class DanhSachBacSiViewModel: BaseViewModel
    {
        private ObservableCollection<BACSI> dsbs;
        public ObservableCollection<BACSI> DSBS
        {
            get => dsbs;
            set
            {
                dsbs = value;
                OnPropertyChanged();
            }
        }

        public DanhSachBacSiViewModel()
        {
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
        }

        void LoadData()
        {
            DSBS = new ObservableCollection<BACSI>();
        }
    }
}
