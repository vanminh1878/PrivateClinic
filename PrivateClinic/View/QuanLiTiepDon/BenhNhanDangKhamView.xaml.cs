using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for BenhNhanDangKhamView.xaml
    /// </summary>
    public partial class BenhNhanDangKhamView : UserControl
    {
        public BenhNhanDangKhamView()
        {
            InitializeComponent();
            BenhNhanDangKhamViewModel viewModel= new BenhNhanDangKhamViewModel();
            this.DataContext = viewModel;
            
        }
        public void SetData(BENHNHAN benhNhan)
        {
            MaBN.Text = benhNhan.MaBN;
            TenBN.Text = benhNhan.HoTen;
            NgsinhBN.Text = benhNhan.NamSinh.ToString("dd/MM/yyyy");
            GioiTinh.Text = benhNhan.GioiTinh;

            DateTime namSinh = benhNhan.NamSinh;
            int tuoi = DateTime.Now.Year - namSinh.Year;
            if (DateTime.Now < namSinh.AddYears(tuoi))
            {
                tuoi--;
            }
            Tuoi.Text = tuoi.ToString();

            NgKham.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BenhNhanDangKhamViewModel viewModel)
            {
                var selectedBenhNhan = (sender as Button)?.CommandParameter as BENHNHAN;

                if (selectedBenhNhan != null)
                {
                    viewModel.DeleteCommand.Execute(selectedBenhNhan);
                    ListViewMed.Items.Refresh();
                }
            }
        }

        private void EditMedButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BenhNhanDangKhamViewModel viewModel)
            {
                var selectedBenhNhan = (sender as Button)?.CommandParameter as THUOC;

                if (selectedBenhNhan != null)
                {
                    viewModel.EditMedCommand.Execute(selectedBenhNhan);
                    ListViewMed.Items.Refresh();
                }
            }
        }
    }
}
