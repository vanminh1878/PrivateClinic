using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.ViewModel.QuanLiKhamBenhVM
{
    public class BenhNhanDaKhamViewModel : BaseViewModel
    {
        #region Các Command và Property
        private ObservableCollection<BenhNhanDTO> listBN;
        public ObservableCollection<BenhNhanDTO> ListBN
        {
            get => listBN; 
            set
            {
                listBN = value;
                OnPropertyChanged(nameof(ListBN));
            }
        }

        private ObservableCollection<BenhNhanDTO> filterlistBN;
        public ObservableCollection<BenhNhanDTO> FilterListBN
        {
            get => filterlistBN;
            set
            {
                filterlistBN = value;
                OnPropertyChanged(nameof(FilterListBN));
            }
        }

        private ObservableCollection<BENHNHAN> benhnhan;
        public ObservableCollection<BENHNHAN> BenhNhan
        {
            get => benhnhan;
            set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(BenhNhan));
            }
        }

        private ObservableCollection<PHIEUKHAMBENH> listpkb;
        public ObservableCollection<PHIEUKHAMBENH> ListPKB
        {
            get => listpkb;
            set
            {
                listpkb = value;
                OnPropertyChanged(nameof(ListPKB));
                SoLuong = ListPKB.Count;
            }
        }

        private ObservableCollection<LOAIBENH> listLoaiBenh;
        public ObservableCollection<LOAIBENH> ListLoaiBenh
        {
            get => listLoaiBenh;
            set
            {
                listLoaiBenh = value;
                OnPropertyChanged(nameof(ListLoaiBenh));
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterDSBN();
                }
            }
        }

        private int soluong;
        public int SoLuong
        {
            get => soluong;
            set
            {
                soluong = value;
                OnPropertyChanged(nameof(SoLuong));
            }
        }
        #endregion

        //Hàm khởi tạo
        public BenhNhanDaKhamViewModel()
        {
            LoadData();
        }

        //Hàm load dữ liệu lên list view 
        void LoadData()
        {
            // Lấy tất cả phiếu khám bệnh
            ListPKB = new ObservableCollection<PHIEUKHAMBENH>(DataProvider.Ins.DB.PHIEUKHAMBENH);
            // Lấy tất cả loại bệnh
            ListLoaiBenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENH);
            // Lấy tất cả bệnh nhân
            BenhNhan = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHAN);

            // Tạo từ điển để tra cứu nhanh
            var pkbDictionary = ListPKB.GroupBy(pkb => pkb.MaBN).ToDictionary(group => group.Key, group => group.ToList());
            var loaiBenhDictionary = ListLoaiBenh.ToDictionary(lb => lb.MaLoaiBenh, lb => lb.TenLoaiBenh);

            // Xử lý
            ListBN = new ObservableCollection<BenhNhanDTO>();
            int stt = 1;

            foreach (var benhnhan in BenhNhan)
            {
                if (pkbDictionary.TryGetValue(benhnhan.MaBN, out var pkbList))
                {
                    foreach (var pkb in pkbList)
                    {
                        BenhNhanDTO benhNhanDTO = new BenhNhanDTO
                        {
                            STT = stt,
                            HoTen = benhnhan.HoTen,
                            TrieuChung = pkb.TrieuChung,
                            NgayKham = pkb.NgayKham,
                            TenLoaiBenh = loaiBenhDictionary.TryGetValue(pkb.MaLoaiBenh, out var tenLoaiBenh) ? tenLoaiBenh : string.Empty
                        };

                        ListBN.Add(benhNhanDTO);
                        stt++;
                    }
                }
            }
            SearchBN();
            
        }

        #region Chức năng search theo tên
        void SearchBN()
        {
            FilterListBN = new ObservableCollection<BenhNhanDTO>(ListBN);//ban đầu thì không cần lọc
        }
        private void FilterDSBN()
        {
            // Cập nhật FilteredDSBS dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterListBN = new ObservableCollection<BenhNhanDTO>(ListBN);
            }
            else
            {
                FilterListBN = new ObservableCollection<BenhNhanDTO>(
                    ListBN.Where(s => s.HoTen.ToLower().Contains(SearchText.ToLower())));
            }
        }
        #endregion

    }
}
