﻿using Furesoft.Proxy.Core;
using Furesoft.Proxy.Core.Attributes;
using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace Furesoft.Proxy.Commands
{
    [SearchableCommand("Add Filter")]
    [KeyBindingCommand("Control+Shift+A")]
    public class AddFilterCommand : MarkupExtension, ICommand
    {
        public static AddFilterCommand Instance = new AddFilterCommand();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CommandContext.IsInContext(CommandContextIds.FilterPage);
        }

        public void Execute(object parameter)
        {
            SearchableCommandRepository.Instance.OpenDialog("Add Filter");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}