﻿using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Services.Interfaces;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace Furesoft.Proxy.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value;OnPropertyChanged(); }
        }

        public string PasswordHash { get; set; }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand((cp) =>
            {
                var wservice = ServiceLocator.Instance.Provider.GetService<IWService>();
                if (wservice.IsRunning())
                {
                    wservice.Stop();
                }

                ServiceLocator.Instance.IsLoggedIn = true;

                NotificationManager.notifier.ShowSuccess("Logged in: " + PasswordHash);
            });
        }
    }
}