using PrivateClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using Button = System.Windows.Controls.Button;
using PrivateClinic.ViewModel.QuanLiTiepDon;

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for QuanLiTiepDonView.xaml
    /// </summary>
    public partial class QuanLiTiepDonView : Page
    {
        public QuanLiTiepDonView()
        {
            InitializeComponent();
            QuanLiTiepDonViewModel viewmodel = new QuanLiTiepDonViewModel();
            this.DataContext = viewmodel;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLiTiepDonViewModel viewModel)
            {
                var selectedBenhNhan = (sender as Button)?.CommandParameter as BENHNHAN;

                if (selectedBenhNhan != null)
                {
                    viewModel.DeleteCommand.Execute(selectedBenhNhan);
                    ListViewBN.Items.Refresh();
                }
            }
        }

        private void EditBNButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLiTiepDonViewModel viewModel)
            {
                var selectedBenhNhan = (sender as Button)?.CommandParameter as BENHNHAN;

                if (selectedBenhNhan != null)
                {
                    viewModel.EditBNCommand.Execute(selectedBenhNhan);
                    ListViewBN.Items.Refresh();
                }
            }
        }

    }
}
