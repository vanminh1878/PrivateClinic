using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace PrivateClinic.ViewModel.BangDieuKhien
{
    public class BangDieuKhienViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<BACSI> _listBS;
        public ObservableCollection<BACSI> listBS
        {
            get => _listBS; set
            {
                _listBS = value;
                OnPropertyChanged();
                UpdateListDoctorCount();
            }
        }

        private int _listDoctorCount;
        public int ListDoctorCount
        {
            get => _listDoctorCount;
            set
            {
                _listDoctorCount = value;
                OnPropertyChanged(nameof(ListDoctorCount));
            }
        }

        public ObservableCollection<string> Months { get; set; }
        private string _selectedMonth;
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
                UpdateChartData();
            }
        }

        // Config Chart
        public SeriesCollection RevenueData { get; set; }
        private string[] _axisXLabels;
        public string[] AxisXLabels
        {
            get { return _axisXLabels; }
            set
            {
                _axisXLabels = value;
                OnPropertyChanged(nameof(AxisXLabels));
            }
        }

        private string[] _axisXLabelsUsage;
        public string[] AxisXLabelsUsage
        {
            get { return _axisXLabelsUsage; }
            set
            {
                _axisXLabelsUsage = value;
                OnPropertyChanged(nameof(AxisXLabelsUsage));
            }
        }


        public SeriesCollection MedicineData { get; set; }
        public SeriesCollection UsageData { get; set; }
        #endregion

        public BangDieuKhienViewModel()
        {
            listBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            Months = new ObservableCollection<string> {"Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5" , "Tháng 6"};
            SelectedMonth = Months[0]; // Default selection
        }

        #region Functions

        #region Thống kê doanh thu
        private void UpdatRevenueChartData()
        {
            var listDoanhThu = new ObservableCollection<BAOCAODOANHTHU>(DataProvider.Ins.DB.BAOCAODOANHTHUs);

            var months = new List<string>();
            var revenues = new ChartValues<double>();

            foreach (var doanhThu in listDoanhThu)
            {
                if (doanhThu.Thang.HasValue && doanhThu.TongDoanhThu.HasValue)
                {
                    months.Add($"{doanhThu.Thang}/{doanhThu.Nam}");
                    revenues.Add(doanhThu.TongDoanhThu.Value);
                }
            }

            RevenueData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh Thu",
                    Values = revenues
                }
            };

            AxisXLabels = months.ToArray();

            Console.WriteLine($"RevenueData: {RevenueData.Count}");
        }
        #endregion

        #region Thống kê Thuốc chart
        private void UpdateMedicineChartData()
        {
            var listThuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            var listLoaiThuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);

            Console.WriteLine($"Total THUOCs: {listThuoc.Count}");
            Console.WriteLine($"Total LOAITHUOCs: {listLoaiThuoc.Count}");
            var pieSeriesList = new List<PieSeries>();
            foreach (var loaiThuoc in listLoaiThuoc)
            {
                int totalSoLuong = 0;

                foreach (var thuoc in listThuoc)
                {
                    if (thuoc.MaLoaiThuoc == loaiThuoc.MaLoaiThuoc)
                    {
                        totalSoLuong += thuoc.SoLuong ?? 0;
                    }
                }
                if (totalSoLuong == 0)
                {
                    continue;
                }

                pieSeriesList.Add(new PieSeries
                {
                    Title = loaiThuoc.TenLoaiThuoc,
                    Values = new ChartValues<int> { totalSoLuong },
                    DataLabels = true
                });
            }

            foreach (var series in pieSeriesList)
            {
                Console.WriteLine($"Series Title: {series.Title}, Values: {string.Join(",", series.Values.Cast<int>())}");
            }


            MedicineData = new SeriesCollection();
            foreach (var pieSeries in pieSeriesList)
            {
                MedicineData.Add(pieSeries);
            }
            Console.WriteLine($"MedicineData: {MedicineData.Count}, medicineData: {pieSeriesList.Count}");
        }
        #endregion

        #region Thống kê sử dụng thuốc


        private void UpdateMedicineUsageChartData(int month)
        {
            var listBaoCaoSuDungThuoc = new ObservableCollection<BAOCAOSUDUNGTHUOC>(DataProvider.Ins.DB.BAOCAOSUDUNGTHUOCs);

            var filteredData = listBaoCaoSuDungThuoc.Where(x => x.Thang == month).ToList();

            var seriesCollection = new SeriesCollection();

            var uniqueMedicines = filteredData.Select(x => x.MaThuoc).Distinct();

            foreach (var medicine in uniqueMedicines)
            {
                var medicineData = filteredData.Where(x => x.MaThuoc == medicine).ToList();
                var values = new ChartValues<int>();

                foreach (var data in medicineData)
                {
                    values.Add(data.SoLanDung ?? 0);
                }

                seriesCollection.Add(new LineSeries
                {
                    Title = $"Thuốc {medicine}",
                    Values = values,
                    DataLabels = true,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15
                });
            }

            UsageData = seriesCollection;

            // Cập nhật Labels cho AxisX
            AxisXLabelsUsage = new[] { $"Tháng {month}" };

            Console.WriteLine($"UsageData: {UsageData.Count}");
        }

        #endregion

        private void UpdateChartData()
        {
            if (RevenueData == null || RevenueData.Count == 0)
            {
                UpdatRevenueChartData();
            }

            if (MedicineData == null || MedicineData.Count == 0)
            {
                UpdateMedicineChartData();
            }

            if (UsageData == null || UsageData.Count == 0)
            {
                UpdateMedicineUsageChartData(1);
            }

            switch (SelectedMonth)
            {
                case "Tháng 3":

                    break;

                case "Tháng 4":


                    break;

                case "Tháng 5":


                    break;
            }
            listBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            #endregion
        }

        private void UpdateListDoctorCount()
        {
            var listDoctorToCount = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            ListDoctorCount = listDoctorToCount.Count;
            OnPropertyChanged(nameof(ListDoctorCount));
        }
    }
}
