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
            Months = new ObservableCollection<string> { "Tháng 3", "Tháng 4", "Tháng 5" };
            SelectedMonth = Months[0];

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

        private void UpdateChartData()
        {
            if (RevenueData == null || RevenueData.Count == 0)
            {
                InitializeChartData();
            }

            if (MedicineData == null || MedicineData.Count == 0)
            {
                InitializeMedicineChartData();
            }

            if (UsageData == null || UsageData.Count == 0)
            {
                InitializeUsageChartData();
            }

            switch (SelectedMonth)
            {
                case "Tháng 3":
                    RevenueData[0].Values = new ChartValues<double> { 40 };
                    RevenueData[1].Values = new ChartValues<double> { 25 };
                    RevenueData[2].Values = new ChartValues<double> { 35 };

                    MedicineData[0].Values = new ChartValues<double> { 10, 50, 39, 50 };
                    UsageData[0].Values = new ChartValues<double> { 20, 36, 18, 40 };
                    break;

                case "Tháng 4":
                    RevenueData[0].Values = new ChartValues<double> { 30 };
                    RevenueData[1].Values = new ChartValues<double> { 20 };
                    RevenueData[2].Values = new ChartValues<double> { 50 };

                    MedicineData[0].Values = new ChartValues<double> { 15, 60, 29, 60 };
                    UsageData[0].Values = new ChartValues<double> { 25, 46, 28, 50 };
                    break;

                case "Tháng 5":
                    RevenueData[0].Values = new ChartValues<double> { 50 };
                    RevenueData[1].Values = new ChartValues<double> { 30 };
                    RevenueData[2].Values = new ChartValues<double> { 20 };

                    MedicineData[0].Values = new ChartValues<double> { 20, 70, 49, 70 };
                    UsageData[0].Values = new ChartValues<double> { 30, 56, 38, 60 };
                    break;
            }
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
