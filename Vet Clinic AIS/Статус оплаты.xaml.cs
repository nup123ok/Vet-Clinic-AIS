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
    /// Логика взаимодействия для Статус_оплаты.xaml
    /// </summary>
    public partial class Статус_оплаты : Window
    {
        private readonly Appointments appointment;
        public Статус_оплаты(Appointments appointment)
        {
            InitializeComponent();
            this.appointment = appointment;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            

            if (int.TryParse(paymentAmountTextBox.Text, out int paymentAmount))
                {
                    if (paymentAmount <= appointment.Cost)
                    {
                        // Вычитаем взнос из стоимости
                        appointment.Cost -= paymentAmount;

                        // Если стоимость оплачена полностью, меняем статус на "Оплачено"
                        if (appointment.Cost == 0)
                        {
                            appointment.StatusPayment = "Оплачено";
                        }
                        // Сохраняем изменения в базе данных или в вашем хранилище данных
                        // Например, bd.SaveChanges();, если bd - ваш контекст базы данных
                    }
                    else
                    {
                        MessageBox.Show("Сумма взноса не может быть больше стоимости приема.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректную сумму взноса.");
                }

            using (VetClinicDB2Entities bd = new VetClinicDB2Entities())
            {
                // Находим в базе данных тот же объект appointment по его ID или идентификатору
                var existingAppointment = bd.Appointments.FirstOrDefault(a => a.ID == appointment.ID);

                if (existingAppointment != null)
                {
                    // Изменяем данные в найденном объекте
                    existingAppointment.Cost -= paymentAmount;

                    if (existingAppointment.Cost == 0)
                    {
                        existingAppointment.StatusPayment = "Оплачено";
                    }

                    try
                    {
                        // Сохраняем изменения в базе данных
                        bd.SaveChanges();
                        MessageBox.Show("Изменения сохранены.");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Прием не найден в базе данных.");
                }
            }
        }
        }
            }
       
    

