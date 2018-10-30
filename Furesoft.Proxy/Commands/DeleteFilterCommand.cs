﻿using Furesoft.Proxy.Core;
using Furesoft.Proxy.Core.Attributes;
using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using System;
using System.Windows.Input;
using System.Windows.Markup;
using ToastNotifications.Messages;

namespace Furesoft.Proxy.Commands
{
    [KeyBindingCommand("Control+Shift+Del")]
    public class DeleteFilterCommand : MarkupExtension, ICommand
    {
        private static DeleteFilterCommand Instance = new DeleteFilterCommand();
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            CommandContext.ContextChanged += () =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            };

            return CommandContext.IsInContext(CommandContextIds.FilterPage, CommandContextIds.FilterSelected);
        }

        //ToDo: repair delete in listbox
        public async void Execute(object parameter)
        {
            var result = (bool)await ServiceLocator.Instance.RpcClient.CallMethodAsync<IFilterOperations>("Remove", ServiceLocator.Instance.SelectedFilter);

            if(result)
            {
                ServiceLocator.Instance.AllFilter.Remove((Filter)parameter);
                NotificationManager.notifier.ShowSuccess("Filter Removed successfully");
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}