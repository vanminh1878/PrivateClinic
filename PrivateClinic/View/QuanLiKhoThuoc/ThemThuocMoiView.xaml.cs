using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThemThuocMoiView.xaml
    /// </summary>
    public partial class ThemThuocMoiView : Window
    {
        public ThemThuocMoiView()
        {
            InitializeComponent();
            ThemThuocViewModel viewmodel = new ThemThuocViewModel();
            this.DataContext = viewmodel;
        }

    }
}
