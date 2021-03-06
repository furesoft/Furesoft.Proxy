﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;

namespace Furesoft.Proxy.Core
{
    public class SearchableCommandRepository
    {
        public static SearchableCommandRepository Instance = new SearchableCommandRepository();
        
        private Dictionary<string, ICommand> _storage = new Dictionary<string, ICommand>();

        public ICommand this[string key]
        {
            get
            {
                return GetCommand(key);
            }
        }

        public Action<string> OpenDialog { get; set; }

        public string FindName(ICommand cmd)
        {
            return _storage.First(_ => _.Value == cmd).Key;
        }

        public void Add(string name, ICommand cmd)
        {
            var usageCombinedCommand = new ActionCommand( _=>
            {
                CommandUsageProvider.Instance.Add(name);

                cmd.Execute(_);
            });

            if (!_storage.ContainsKey(name))
            {
                _storage.Add(name, usageCombinedCommand);
            }
            else
            {
                _storage[name] = usageCombinedCommand;
            }
            CommandUsageProvider.Instance.Add(name);
        }
        public void AddDialogCommand(string name)
        {
            Instance.Add(name, (_) =>
            {
                Instance.OpenDialog(name);
            });
        }

        public void CombineCommands(string name, ICommand first, ICommand second)
        {
            Add(name, new ActionCommand( _=>
            {
                first.Execute(_);
                second.Execute(_);
            }));
        }

        public void Add(string name, Action<object> callback)
        {
            Add(name, new ActionCommand(callback));
        }

        public ICommand GetCommand(string name)
        {
            if (_storage.ContainsKey(name)) return _storage[name];

            throw new KeyNotFoundException($"Command '{name}' not found");
        }

        public void ExecuteCommand(string name, object arg)
        {
            GetCommand(name).Execute(arg);
        }

        public string[] CommandNames
        {
            get
            {
                return _storage.Keys.ToArray();
            }
        }

        public Action<object> OpenCustomDialog { get; set; }
    }

    public class CommandRepository : MarkupExtension
    {
        public string Name { get; set; }

        public CommandRepository(string name)
        {
            Name = name;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return SearchableCommandRepository.Instance.GetCommand(Name);
        }
    }
}