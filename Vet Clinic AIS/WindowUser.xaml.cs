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

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        private UserClass currentUser;
        public WindowUser(UserClass user)
        {
            InitializeComponent();
            currentUser = user;
            mainFrame.Navigate(new PriemAddPage(currentUser));
            Manager.mainFrame = mainFrame;
        }

        private void pr_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PriemAddPage(currentUser));
        }

        private void cl_btn(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new Profile(currentUser));
        }

        
    }
}
