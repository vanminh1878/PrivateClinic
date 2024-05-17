﻿using PrivateClinic.Model;
using PrivateClinic.ViewModel.HoSoBacSiVM;
using System;
using System.Collections;
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

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for ThongTinChiTietCuaMotBacSIView.xaml
    /// </summary>
    public partial class ThongTinChiTietCuaMotBacSIView : Window
    {
        public ThongTinChiTietCuaMotBacSIView()
        {
            InitializeComponent();
            ThongTinChiTietCuaMotBacSiViewModel doctorDetailViewModel = new ThongTinChiTietCuaMotBacSiViewModel();
            this.DataContext = doctorDetailViewModel;
        }
    }
}
