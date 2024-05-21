using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class BenhNhanDangKhamViewModel: BaseViewModel
    {
        private ObservableCollection<THUOC> _listMed;
        public ObservableCollection<THUOC> listMed { get => _listMed; set { _listMed = value; OnPropertyChanged(); } }

        //Lấy danh sách  thuốc DTO
        private ObservableCollection<ThuocDTO> listThuoc;
        public ObservableCollection<ThuocDTO> ListThuoc
        {
            get => listThuoc;
            set
            {
                listThuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }

        private ObservableCollection<ThuocDTO> listThuocView;
        public ObservableCollection<ThuocDTO> ListThuocView
        {
            get => listThuocView;
            set
            {
                listThuocView = value;
                OnPropertyChanged(nameof(ListThuocView));
                SoLuongThuocDangChon = ListThuocView.Count;
            }
        }
        //Lấy danh sách các loại bệnh
        private ObservableCollection<LOAIBENH> listloaibenh;
        public ObservableCollection<LOAIBENH> ListLoaiBenh
        {
            get => listloaibenh;
            set
            {
                listloaibenh = value;
                OnPropertyChanged(nameof(ListLoaiBenh));
            }
        }
        //Lấy đối tượng LoaiBenh
        private LOAIBENH selectedLoaiBenh;
        public LOAIBENH SelectedLoaiBenh
        {
            get => selectedLoaiBenh;
            set
            {
                selectedLoaiBenh = value;
                OnPropertyChanged(nameof (SelectedLoaiBenh));
            }
        }


        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditThuocCommand { get; set; }
        private int soLuongThuocDangChon;
        public int SoLuongThuocDangChon
        {
            get => soLuongThuocDangChon;
            set
            {
                soLuongThuocDangChon = value;
                OnPropertyChanged(nameof(SoLuongThuocDangChon));
            }
        }
        public BenhNhanDangKhamViewModel()
        {
            ListLoaiBenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENH);
            listMed = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOC);
            AddThuoc();
            EditThuoc();
            ListThuoc = new ObservableCollection<ThuocDTO>();
            SearchCommand = new RelayCommand<BenhNhanDangKhamView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            DeleteCommand = new RelayCommand<ThuocDTO>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
        }
        void AddThuoc()
        {
            AddCommand = new ViewModelCommand(showAdd);
        }
        private void showAdd(object obj)
        {
            ThemThuocChoBenhNhanView view = new ThemThuocChoBenhNhanView();
            view.ShowDialog();
            LoadData();
        }
        #region Chức năng sửa 1 bác sĩ
        void EditThuoc()
        {
            EditThuocCommand = new ViewModelCommand(ShowWDEditThuoc);
        }
        private void ShowWDEditThuoc(object obj)
        {
            ThuocDTO thuoc = obj as ThuocDTO;
            SuaThongTinDonThuocView view = new SuaThongTinDonThuocView((ThuocDTO)obj);
            view.ShowDialog();
            LoadData();
        }

        #endregion
        void _SearchCommand(BenhNhanDangKhamView parameter)
        {
            ObservableCollection<ThuocDTO> temp = new ObservableCollection<ThuocDTO>();
            if (ListThuocView != null)
            {
                if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
                {
                    string searchKeyword = parameter.txbSearch.Text.ToLower();
                    temp = new ObservableCollection<ThuocDTO>(ListThuocView.Where(s => s.TenThuoc.ToLower().Contains(searchKeyword)));
                }
                else
                {
                    temp = new ObservableCollection<ThuocDTO>(ListThuocView);
                }
                parameter.ListViewMed.ItemsSource = temp;
            }
            
        }
        void _DeleteCommand(ThuocDTO selectedItem)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Bạn muốn xóa thuốc này không ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    Const.ListThuocTemp.Remove(selectedItem);
                    MessageBox.Show("Xóa thành công", "Thông báo");
                    SoLuongThuocDangChon = Const.ListThuocTemp.Count;
                }
            }
        }

        void LoadData()
        {
            if (ListThuocView == null)
            {
                ListThuocView = new ObservableCollection<ThuocDTO>();
            }
            // Add items from ListThuoc to ListThuocView
            if (Const.ListThuoc == null)
                return;
            else
            {
                foreach (var thuocDTO in Const.ListThuoc)
                {
                    ListThuocView.Add(thuocDTO);
                }
                Const.ListThuoc.Clear();
                Const.ListThuocTemp = new ObservableCollection<ThuocDTO>(ListThuocView);
            }
            ListThuocView = Const.ListThuocTemp;
            SoLuongThuocDangChon = ListThuocView.Count;


        }
        
    }
}
