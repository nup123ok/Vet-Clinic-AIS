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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        VetClinicDB2Entities bd = new VetClinicDB2Entities();
        private UserClass currentUser;
        public Profile(UserClass user)
        {
            InitializeComponent();
            currentUser = user;
            UserNametxt.Text += currentUser.Username;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newPassword = txtPassword.Password;

                // Обновите пароль пользователя в базе данных
                
                    var userToUpdate = bd.Users.Find(currentUser.Id); // Предполагая, что у вас есть Id пользователя
                    if (userToUpdate != null)
                    {
                        userToUpdate.Password = newPassword; // Обновите пароль на новый
                        bd.SaveChanges(); // Сохраните изменения в базе данных
                    }
                

                MessageBox.Show("Пароль успешно изменен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при смене пароля: " + ex.Message);
            }
        }

        private void Ex_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из профиля?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Window win = Window.GetWindow(this);
            var newForm = new RegLog();
            newForm.Show();
            if(win != null)
            {
                win.Close();
            }
            }
            
            
        }
    }
}
