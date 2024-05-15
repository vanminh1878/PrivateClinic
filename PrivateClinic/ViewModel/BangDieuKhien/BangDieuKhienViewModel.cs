using LiveCharts;
using LiveCharts.Wpf;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrivateClinic.ViewModel.BangDieuKhien
{
    public class BangDieuKhienViewModel : BaseViewModel
    {

        #region Properties
        public ObservableCollection<Doctor> listBS { get; set; }

        //Config Chart
        public SeriesCollection RevenueData { get; set; }
        public SeriesCollection MedicineData { get; set; }
        public SeriesCollection UsageData { get; set; }
        #endregion

        public BangDieuKhienViewModel()
        {
            listBS = new ObservableCollection<Doctor>()
        {
            new Doctor { Name = "Trần Thu Hằng", StatusText = "Nhan vien" },
            new Doctor { Name = "Nguyễn Văn A", StatusText = "Nhan vien" },
            new Doctor { Name = "Nguyễn Văn A", StatusText = "Nhan vien" }
        };
            InitializeChartData();
            InitializeMedicineChartData();
            InitializeUsageChartData();
        }


        #region Functions
        //Config Chart
        private void InitializeChartData()
        {
            RevenueData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "TIEN THUOC",
                    Values = new ChartValues<double> { 40 },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "TIEN KHAM",
                    Values = new ChartValues<double> { 25 },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "KHAC",
                    Values = new ChartValues<double> { 35 },
                    DataLabels = true
                }
            };
        }

        private void InitializeMedicineChartData()
        {
            MedicineData = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "2022",
                Values = new ChartValues<double> { 10, 50, 39, 50 }
            }
        };
        }

        private void InitializeUsageChartData()
        {
            UsageData = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Usage",
                Values = new ChartValues<double> { 20, 36, 18, 40 }
            }
        };
        }
        #endregion


        //Dummy
        public class Doctor
        {
            public string Name { get; set; }
            public string StatusText { get; set; }
        }
    }
}
