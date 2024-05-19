using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;
using System.Windows.Controls;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for QuanLiKhoThuocView.xaml
    /// </summary>
    public partial class QuanLiKhoThuocView : Page
    {
        public QuanLiKhoThuocView()
        {
            InitializeComponent();
            QuanLyThuocViewModel viewModel = new QuanLyThuocViewModel();
            this.DataContext = viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLyThuocViewModel viewModel)
            {
                var selectedMedicine = (sender as Button)?.CommandParameter as THUOC;

                if (selectedMedicine != null)
                {
                    viewModel.DeleteMedicineCommand.Execute(selectedMedicine);
                    MedicineListView.Items.Refresh();
                }
            }
        }

        private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLyThuocViewModel viewModel)
            {
                var selectedMedicine = (sender as Button)?.CommandParameter as THUOC;

                if (selectedMedicine != null)
                {
                    viewModel.EditMedicineCommand.Execute(selectedMedicine);
                    MedicineListView.Items.Refresh();
                }
            }
        }
    }
}
