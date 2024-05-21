using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows.Controls;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThemThuocMoiView.xaml
    /// </summary>
    public partial class ThemThuocMoiView : UserControl
    {
        public ThemThuocMoiView()
        {
            InitializeComponent();
            ThemThuocMoiViewModel viewModel = new ThemThuocMoiViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
            TenDVTcbx.ItemsSource = viewModel.TenDVTs;
        }
    }
}
