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
using Vet_Clinic_AIS;

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для Приемы.xaml
    /// </summary>
    public partial class Приемы : Page
        
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        public Приемы()
        {
            InitializeComponent();
            СLDG.ItemsSource = bd.Appointments.ToList();
        }


        VetClinicDB2Entities db = new VetClinicDB2Entities();
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (СLDG.SelectedItem != null && СLDG.SelectedItem is Appointments selectedAppointment)
            {
                // Создаем окно ввода суммы взноса, передавая выбранный прием
                Статус_оплаты paymentWindow = new Статус_оплаты(selectedAppointment);
                paymentWindow.ShowDialog();
            }


        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            СLDG.ItemsSource = bd.Appointments.ToList();
        }
    }
}
