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
            Main.Content = new KhoThuocView();
            QuanLiKhoThuocViewModel viewModel = new QuanLiKhoThuocViewModel();
            this.DataContext = viewModel;
        }

        


       
    }
}
