﻿using Furesoft.Proxy.Core;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using Furesoft.Proxy.UI;
using System.ComponentModel;
using System.Threading;

namespace Furesoft.Proxy
{
    public class ServiceLocator : INotifyPropertyChanged
    {
        public static ServiceLocator Instance = new ServiceLocator();

        public ServiceProvider Provider = new ServiceProvider();

        public Filter[] AllFilter;

        private bool _loggedIn;

        public bool IsLoggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoggedIn)));
            }
        }

        public PageTransition PageContainer = new PageTransition();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}