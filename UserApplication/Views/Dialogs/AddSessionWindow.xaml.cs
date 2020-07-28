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
    /// Логика взаимодействия для AddSessionWindow.xaml
    /// </summary>
    public partial class AddSessionWindow : Window
    {
        public AddSessionWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;
        }

        private async void button_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var d = datePicker.SelectedDate;
                DateTime dateTime = (DateTime)datePicker.SelectedDate;
                var h = int.Parse(textBox_hours.Text);
                var m = int.Parse(textBox_minutes.Text);
                dateTime = dateTime.AddHours(h);
                dateTime = dateTime.AddMinutes(m);

                SessionModelView model = new SessionModelView
                {
                    Img = textBox_img.Text,
                    NameFilm = textBox_name.Text,
                    Room = textBox_room.Text,
                    NumberSeats = int.Parse(textBox_seats.Text),
                    PriceTicket = double.Parse(textBox_price.Text),
                    StartFilm = dateTime,
                };
                var status = await Request.SaveSession(model);
                if (!status)
                {
                    MessageBox.Show("Ошибка сохранения");
                }
                else
                {
                    Close();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
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
    }
}
