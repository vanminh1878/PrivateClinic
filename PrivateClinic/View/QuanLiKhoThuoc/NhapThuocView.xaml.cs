using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for NhapThuocView.xaml
    /// </summary>
    public partial class NhapThuocView : Window
    {
        public NhapThuocView()
        {
            InitializeComponent();
            Main.Content= new ThemThuocCuView();
            ThuocCuRBtn.IsChecked = true;
            ThemThuocViewModel viewModel = new ThemThuocViewModel();
            this.DataContext = viewModel;
        }
    }
}
