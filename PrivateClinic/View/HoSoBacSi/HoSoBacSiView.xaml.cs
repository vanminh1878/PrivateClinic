using PrivateClinic.Model;
using PrivateClinic.ViewModel.HoSoBacSiVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for HoSoBacSiView.xaml
    /// </summary>
    public partial class HoSoBacSiView : Page
    {
        public HoSoBacSiView()
        {
            InitializeComponent();
            HoSoBacSiViewModel viewModel = new HoSoBacSiViewModel();
            Main.Content = new ThongTinCaNhanView(viewModel);
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);

        }
    }
}
