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

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserClass currentUser;
        public MainWindow(UserClass user)
        {
            InitializeComponent();
            currentUser = user;
            mainFrame.Navigate(new Клиенты());
            Manager.mainFrame = mainFrame;
        }

        private void pr_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Приемы());
        }

        private void cl_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Клиенты());
        }

        private void pa_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Пациенты());
        }

        private void protsedure_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Процедуры());
        }

        private void role_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Роли());
        }

        private void prof_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Profile(currentUser));
        }
    }
}
