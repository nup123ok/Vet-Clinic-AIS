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
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        private Clients _currentclients = new Clients();
        public AddClientPage(Clients selectedmanicure)
        {
            InitializeComponent();
            if (selectedmanicure != null)
            _currentclients = selectedmanicure;
            DataContext = _currentclients;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentclients.FirstName))
            {
                errors.AppendLine("Заполните поле: ФИО клиента");
            }

            if (string.IsNullOrWhiteSpace(_currentclients.Address))
            {
                errors.AppendLine("Заполните поле: Адрес проживания");
            }

            if (string.IsNullOrWhiteSpace(_currentclients.ContactPhone))
            {
                errors.AppendLine("Заполните поле: Контактный номер телефона");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentclients.ID == 0)
            {
                bd.Clients.Add(_currentclients);
            }
            else
            {
                // В случае редактирования, измените существующую запись
                var existingClient = bd.Clients.Find(_currentclients.ID);
                if (existingClient != null)
                {
                    bd.Entry(existingClient).CurrentValues.SetValues(_currentclients);
                }
            }
            try
            {
                bd.SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 1)
            {
                txtName.Text = txtName.Text.ToUpper();
                txtName.SelectionStart = txtName.Text.Length;
            }
        }
      
    }
}
