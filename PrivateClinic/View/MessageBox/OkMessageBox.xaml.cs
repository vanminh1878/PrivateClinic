﻿using System;
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

namespace PrivateClinic.View.MessageBox
{
    /// <summary>
    /// Interaction logic for OkMessageBox.xaml
    /// </summary>
    public partial class OkMessageBox : Window
    {
        public OkMessageBox(string Title, string Message)
        {
            InitializeComponent();
            txtChuDe.Text = Title;
            txtMessage.Text = Message;
            System.Media.SystemSounds.Hand.Play();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
