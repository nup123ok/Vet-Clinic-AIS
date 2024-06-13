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
    /// Логика взаимодействия для RegLog.xaml
    /// </summary>
    public partial class RegLog : Window
    {
        public RegLog()
        {
            InitializeComponent();
        }
        VetClinicDB2Entities db = new VetClinicDB2Entities();
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text;
            string password = PasswordTb.Password;
            
            try
            {
                Users user = db.Users.Where((u) => u.UserName == login && u.Password == password).Single();
               if (user != null)
                {
                    UserClass currentUser = new UserClass
                    {
                        Id = user.UserID,
                        Username = user.UserName,
                        Password = user.Password
                        
                    };
                    if (user.Role == "Пользователь")
                {
                    var newForm = new WindowUser(currentUser);
                    newForm.Show();
                    this.Close();
                }
                else if(user.Role == "Администратор")
                {
                    var newForm = new MainWindow(currentUser); 
            newForm.Show(); 
            this.Close();
                }
                }

               
               else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
            
        }


    }
}
