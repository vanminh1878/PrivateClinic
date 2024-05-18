using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class QuanLyThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _listThuoc;

        public ObservableCollection<THUOC> ListThuoc
        {
            get => _listThuoc;
            set { _listThuoc = value; OnPropertyChanged(); }

        }


        public ICommand AddMedicineCommand { get; set; }
        public ICommand EditMedicineCommand { get; set; }
        public ICommand DeleteMedicineCommand { get; set; }

        public QuanLyThuocViewModel()
        {
            _listThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            AddMedicineCommand = new RelayCommand<QuanLyThuocView>((p) => { return p == null ? false : true; }, (p) => _AddMedicineCommand(p));
        }

        void _AddMedicineCommand(QuanLyThuocView parameter)
        {
            ThemThuocMoiView addMedicineView = new ThemThuocMoiView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            addMedicineView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addMedicineView.ShowDialog();

        }

    }
}
