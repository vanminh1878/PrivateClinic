
using PrivateClinic.View.OtherViews;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.View.ThanhToan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.OtherViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //field
        public static Frame MainFrame { get; set; }
        //Command ThanhToanCM
        public ICommand CloseLogin { get; set; }
        public ICommand MinimizeLogin { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand BangdieukhienCM { get; set; }
        public ICommand TiepDonCM { get; set; }
        public ICommand KhamBenhCM { get; set; }
        public ICommand ThanhToanCM { get; set; }
        public ICommand KhoThuocCM { get; set; }
        public ICommand BacSiCM { get; set; }
        public ICommand LogOutCM { get; set; }
        public ICommand LoadPageCM { get; set; }
        public ICommand Loadwd { get; set; }

        public ICommand HomeCM { get; set; }

        //property

        public MainViewModel()
        {
            CloseLogin = new RelayCommand<MainView>((p) => true, (p) => Close(p));
            MinimizeLogin = new RelayCommand<MainView>((p) => true, (p) => Minimize(p));
            MoveWindow = new RelayCommand<MainView>((p) => true, (p) => moveWindow(p));
            //Loadwd = new RelayCommand<MainView>((p) => true, (p) => _Loadwd(p));
            LoadPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new QuanLiTiepDonView();
            });
            HomeCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new QuanLiTiepDonView();
            });

            TiepDonCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new QuanLiTiepDonView();
            });
                      
            ThanhToanCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ThanhToanView();
            });



            LogOutCM = new RelayCommand<FrameworkElement>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetParentWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Hide();
                    LoginView w1 = new LoginView();
                    w1.ShowDialog();
                    w.Close();
                }
            });
        }
        private FrameworkElement GetParentWindow(FrameworkElement p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

        public void moveWindow(MainView p)
        {
            p.DragMove();
        }
        public void Close(MainView p)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void Minimize(MainView p)
        {
            p.WindowState = WindowState.Minimized;
        }

    }
}
