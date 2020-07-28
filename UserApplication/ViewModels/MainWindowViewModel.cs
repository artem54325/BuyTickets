using BuyingTicketCore.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using UserApplication.Helpers;
using UserApplication.Views;
using UserApplication.Views.Dialogs;

namespace UserApplication.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            TimerStart();
        }
        #region property

        /// <summary>
        /// Represent DisplayName property
        /// </summary>
        public string Link
        {
            get => _link;
            set => SetProperty(ref _link, value);
        }

        private string _link = "https://192.168.0.151:4475";

        public List<SessionModel> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }
        private List<SessionModel> _sessions = new List<SessionModel>();

        private SessionModel _selectSession;
        public SessionModel SelectSession
        {
            get => _selectSession;
            set
            {
                SetProperty(ref _selectSession, value);
                _selectSession = value;
                OnPropertyChanged("SelectedSession");
                // Обновить список
                if(value != null)
                {
                    AddTicketWindow addTicket = new AddTicketWindow(value);
                    addTicket.ShowDialog();
                }
            }
        }

        #endregion

        #region Commands
        private DelegateCommand<object> _updateCommand;
        public DelegateCommand<object> UpdateCommand => _updateCommand ?? (_updateCommand = new DelegateCommand<object>(ExecuteUpdateTableCommand));

        private DelegateCommand<object> _addSessionCommand;
        public DelegateCommand<object> AddSessionCommand => _addSessionCommand ?? (_addSessionCommand = new DelegateCommand<object>(ExecuteAddSessionCommand));


        #endregion

        #region  Excutes
        void ExecuteUpdateTableCommand(object parameter)
        {
            this.UpdateTable(null, null);
            // Сделать обновление таблицы
        }

        void ExecuteAddSessionCommand(object parameter)
        {
            AddSessionWindow window = new AddSessionWindow();
            window.ShowDialog();
            // Сделать обновление таблицы
            this.UpdateTable(null, null);
        }


        #endregion

        private DispatcherTimer timer = null;
        private void TimerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTable);
            timer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            timer.Start();
        }

        private async void UpdateTable(object sender, EventArgs e)
        {
            try
            {
                Sessions = await Request.GetSession();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
