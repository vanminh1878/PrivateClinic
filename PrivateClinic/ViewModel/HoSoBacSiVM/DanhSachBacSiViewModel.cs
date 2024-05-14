using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                OnPropertyChanged(nameof(DSBS));
            }
        }


        //Chức năng tìm kiếm

        private ObservableCollection<BACSI> filterDSBS;
        public ObservableCollection<BACSI> FilterDSBS 
        { 
            get => filterDSBS; 
            set
            {
                if(filterDSBS != value)
                {
                    filterDSBS = value;
                    OnPropertyChanged(nameof(FilterDSBS));
                }
            }
        }

        private string searchText;
        public string SearchText
        { get => searchText;
            set
        {
                if(searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterDSBacSi();
                }
            }
        }
        void SearchBS()
        {
            FilterDSBS = new ObservableCollection<BACSI>(DSBS);//ban đầu thì không cần lọc
        }
        private void FilterDSBacSi()
        {
            // Cập nhật FilteredDSBS dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSBS = new ObservableCollection<BACSI>(DSBS);
            }
            else
            {
                FilterDSBS = new ObservableCollection<BACSI>(
                    DSBS.Where(s => s.HoTen.ToLower().Contains(SearchText.ToLower())));
            }
        }

        //Hàm khởi tạo
        public DanhSachBacSiViewModel()
        {
           LoadData();
           AddDoctor();
           EditDoctor();
           
        }

        //Chức năng hiển thị danh sách bác sĩ
        void LoadData()
        {
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            SearchBS();
        }

        // Chức năng thêm mới 1 bác sĩ
        public ICommand ShowWDAddDoctor {  get; set; }

        private void ShowWDDoctor(object obj)
        {
            ThemBacSiView view = new ThemBacSiView();
            LoadData();
            view.ShowDialog();
        }

        void AddDoctor()
        {
            ShowWDAddDoctor = new ViewModelCommand(ShowWDDoctor);
        }

        // Chức năng sửa thông tin cho 1 bác sĩ
        public ICommand EditDoctorCommand {  get; set; }
        void EditDoctor()
        {
            EditDoctorCommand = new ViewModelCommand(ShowWDEditDoctor);
        }
        private void ShowWDEditDoctor(object obj)
        {
            SuaThongTinBacSiView view = new SuaThongTinBacSiView();
            view.ShowDialog();
            LoadData();
        }
    } 
}
