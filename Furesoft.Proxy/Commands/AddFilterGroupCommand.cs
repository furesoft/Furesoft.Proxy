﻿using Furesoft.Proxy.Core;
using System;
using System.Windows.Input;

namespace Furesoft.Proxy.Commands
{
    [SearchableCommand("Add Filter Group")]
    public class AddFilterGroupCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SearchableCommandRepository.Instance.OpenDialog("Add Filtergroup");
        }
    }
}