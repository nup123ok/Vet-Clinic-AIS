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
    /// Логика взаимодействия для Клиенты.xaml
    /// </summary>
    public partial class Клиенты : Page
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        public Клиенты()
        {
            InitializeComponent();
            СLDG.ItemsSource = bd.Clients.ToList();
        }

        private void searchingBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchingText = poisk.Text;

            List<Clients> filteredClients = new List<Clients>();

            if (!string.IsNullOrWhiteSpace(searchingText))
            {
                // Преобразование введенного текста и имен клиентов в нижний регистр для сравнения
                string searchTextLower = searchingText.ToLower();

                filteredClients = bd.Clients.Where(p => p.FirstName.ToLower().Contains(searchTextLower)).ToList();
            }
            else
            {
                filteredClients = bd.Clients.ToList();
            }

            // Присвоение отфильтрованных данных элементу управления
            СLDG.ItemsSource = filteredClients;
        }

        private void AddCL_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new AddClientPage(null));
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            var manicureForRemoving = СLDG.SelectedItems.Cast<Clients>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {manicureForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    bd.Clients.RemoveRange(manicureForRemoving);
                    bd.SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    СLDG.ItemsSource = bd.Clients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new AddClientPage((sender as Button).DataContext as Clients));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           if (Visibility == Visibility.Visible)
            {
                bd.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                СLDG.ItemsSource = bd.Clients.ToList();
            }
        }

    }
}
