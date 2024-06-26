﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
    /// Interaction logic for YesNoMessageBox.xaml
    /// </summary>
    public partial class YesNoMessageBox : Window
    {
        public YesNoMessageBox(string Title, string Message)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            txtChuDe.Text = Title;
            System.Media.SystemSounds.Hand.Play();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
