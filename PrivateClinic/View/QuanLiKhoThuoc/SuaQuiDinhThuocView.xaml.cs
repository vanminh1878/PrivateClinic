using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThayDoiQuiDinhThuocView.xaml
    /// </summary>
    public partial class ThayDoiQuiDinhThuocView : Window
    {
        public ThayDoiQuiDinhThuocView()
        {
            InitializeComponent();
            SuaQuiDinhThuocViewModel viewModel =   new SuaQuiDinhThuocViewModel();
            this.DataContext = viewModel;
        }
    }
}
