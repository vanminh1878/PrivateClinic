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

        //
        public DanhSachBacSiViewModel()
        {
           LoadData();
           
        }

        //Load danh sách bác sĩ
        void LoadData()
        {
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            SearchBS();
        }
    }
}
