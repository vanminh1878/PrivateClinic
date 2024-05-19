using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThayDoiThongTinThuocView.xaml
    /// </summary>
    public partial class ThayDoiThongTinThuocView : Window
    {
        public ThayDoiThongTinThuocView()
        {
            InitializeComponent();
            ThayDoiThongTinThuocViewModel viewModel = new ThayDoiThongTinThuocViewModel();
            this.DataContext = viewModel;
        }
    }
}
