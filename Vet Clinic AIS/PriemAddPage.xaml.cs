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
using System.Data.Entity.Validation;

namespace Vet_Clinic_AIS
{
    /// <summary>
    /// Логика взаимодействия для PriemAddPage.xaml
    /// </summary>
    public partial class PriemAddPage : Page
    {
        
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        
        private UserClass currentUser;
        public PriemAddPage(UserClass user)
        {
            InitializeComponent();
            currentUser = user;
            procedureComboBox.ItemsSource = bd.Procedur.ToList();
            procedureComboBox2.ItemsSource = bd.Procedur.ToList();
            patsientComboBox.ItemsSource = bd.Patients.ToList();

        }

        private void UpdateDescription()
        {
            txtdesc.Content = "Врач: " + currentUser.Username + ", " + procedureComboBox.Text + ";" + procedureComboBox2.Text;
        }
        private void CalculateTotalCost()

        {
            if (procedureComboBox.SelectedItem != null && procedureComboBox2.SelectedItem == null)
            {
                var procedure = (Procedur)procedureComboBox.SelectedItem;
                txtcost.Text = procedure.Price.ToString();

                // Обновление текста описания

            }
           if (procedureComboBox.SelectedItem != null && procedureComboBox2.SelectedItem != null)
            {
                var procedure1 = (Procedur)procedureComboBox.SelectedItem;
                var procedure2 = (Procedur)procedureComboBox2.SelectedItem;

                txtcost.Text = (procedure1.Price + procedure2.Price).ToString();

                // Обновление текста описания
                
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (patsientComboBox.SelectedItem != null && clientComboBox.SelectedItem != null)
            {
                Appointments ap = new Appointments();
            if (ap.ID == 0)
            {
                ap.DateTime = DateTime.Now;
                var pet = patsientComboBox.SelectedItem as Patients;
                ap.Patients = bd.Patients.Find(pet.ID);
                var cl = clientComboBox.SelectedItem as Clients;
                ap.Clients = bd.Clients.Find(cl.ID);
                ap.Cost = Convert.ToInt32(txtcost.Text);
                ap.Description = txtdesc.Content.ToString();
                    ap.StatusPayment = "В ожидании";
                    bd.Appointments.Add(ap);
            }
            try
            {

                bd.SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                    patsientComboBox.SelectedItem = null;
                    clientComboBox.SelectedItem = null;
                    procedureComboBox.SelectedItem = null;
                    procedureComboBox2.SelectedItem = null;
                    txtcost.Text = string.Empty;
                    txtdesc.Content = string.Empty;


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
          
        }

        private void patsientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (patsientComboBox.SelectedItem != null)
            {
                var selectedPatient = (Patients)patsientComboBox.SelectedItem;

                // Фильтрация клиентов на основе выбранного пациента
                var clientsForPatient = bd.Clients.Where(client => client.ID == selectedPatient.ClientID).ToList();
                clientComboBox.ItemsSource = clientsForPatient;
            }
            else
            {
                // Если пациент не выбран, показать всех клиентов
                clientComboBox.ItemsSource = bd.Clients.ToList();
            }
        }
        private void UpdateClients()
        {
          
        }

        private void cmbService1_DropDownClosed(object sender, EventArgs e)
        {
            UpdateDescription();
            CalculateTotalCost();
        }

        private void cmbService2_DropDownClosed(object sender, EventArgs e)
        {
            UpdateDescription();
            CalculateTotalCost();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            patsientComboBox.SelectedItem = null;
            clientComboBox.SelectedItem = null;
            procedureComboBox.SelectedItem = null;
            procedureComboBox2.SelectedItem = null;
            txtcost.Text = string.Empty;
            txtdesc.Content = string.Empty;
        }
    }
}
