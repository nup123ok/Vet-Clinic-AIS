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
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для PatsientAddPage.xaml
    /// </summary>
    public partial class PatsientAddPage : Page
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        private Patients _currentclients = new Patients();
        public PatsientAddPage(Patients selectedmanicure)
        {
            InitializeComponent();
            if (selectedmanicure != null)
                _currentclients = selectedmanicure;

            DataContext = _currentclients;
            clientComboBox1.ItemsSource = bd.Clients.ToList();
            clientComboBox2.ItemsSource = bd.Clients.ToList();
           
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
           
            if (string.IsNullOrWhiteSpace(_currentclients.Name))
            {
                errors.AppendLine("Заполните поле: Имя пациента");
            }

            if (string.IsNullOrWhiteSpace(_currentclients.AnimalType))
            {
                errors.AppendLine("Заполните поле: Вид");
            }

            if (cmbgender.SelectedItem == null)
            {
                errors.AppendLine("Заполните поле: Пол");
            }
            if (clientComboBox1.Text == "")
            {
                errors.AppendLine("Заполните поле: Хозяин(ы)");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentclients.ID == 0)
            {
               
                _currentclients.Gender = cmbgender.Text;
                bd.Patients.Add(_currentclients);
            }
            else
            {
                // В случае редактирования, измените существующую запись
                var existingClient = bd.Patients.Find(_currentclients.ID);
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
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.GoBack();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 1)
            {
                txtName.Text = txtName.Text.ToUpper();
                txtName.SelectionStart = txtName.Text.Length;
            }
        }

        private void txtType_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txttype.Text.Length == 1)
            {
                txttype.Text = txttype.Text.ToUpper();
                txttype.SelectionStart = txttype.Text.Length;
            }
        }
    }
}
