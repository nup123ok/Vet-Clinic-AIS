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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для Процедуры.xaml
    /// </summary>
    public partial class Процедуры : Page
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        public Процедуры()
        {
            InitializeComponent();
            СLDG.ItemsSource = bd.Procedur.ToList();
        }

        private void AddCL_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ProtseduraAddPage(null));
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
