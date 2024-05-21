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
            ThemThuocViewModel viewModel = new ThemThuocViewModel();
            this.DataContext = viewModel;
        }
    }
}
