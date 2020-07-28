using BuyingTicketCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserApplication.Helpers;

namespace UserApplication.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddTicketWindow.xaml
    /// </summary>
    public partial class AddTicketWindow : Window
    {
        private SessionModel value;

        public AddTicketWindow(SessionModel value)
        {
            this.value = value;
            InitializeComponent();
            label_nameFilm.Content = value.NameFilm;
            label_start_film.Content = value.StartFilm;
            label_nameFilm.Content = value.NameFilm;
        }

        private void textBox_count_ticket(object sender, TextChangedEventArgs e)
        {
            try
            {
                int countTicket = int.Parse(textBox.Text);
                label_cost.Content = "Цена билета: " + countTicket * value.PriceTicket;
            }
            catch(Exception)
            {
                label_cost.Content = "Цена билета: ";
            }
        }

        void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsGood))
                e.CancelCommand();
        }

        bool IsGood(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            if (c >= 'a' && c <= 'f')
                return true;
            if (c >= 'A' && c <= 'F')
                return true;
            return false;
        }

        private async void button_buy_ticket(object sender, RoutedEventArgs e)
        {
            try
            {
                int countTicket = int.Parse(textBox.Text);
                var status = await Request.BuyTicket(value.Id, countTicket);
                if (!status)
                {
                    MessageBox.Show("Ошибка при покупке билета");
                }
                Close();
            }
            catch (Exception)
            {
                label_cost.Content = "Цена билета: ";
            }
        }
    }
}
