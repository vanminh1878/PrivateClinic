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
        }

    }
}
