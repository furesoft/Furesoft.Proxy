using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Markup;

namespace Furesoft.Proxy.Core
{
    public class SearchableCommandRepository
    {
        public static SearchableCommandRepository Instance = new SearchableCommandRepository();

        private Dictionary<string, ICommand> _storage = new Dictionary<string, ICommand>();

        public void Add(string name, ICommand cmd)
        {
            if (!_storage.ContainsKey(name))
            {
                _storage.Add(name, cmd);
            }
            _storage[name] = cmd;
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

        public string[] CommandNames
        {
            get
            {
                return _storage.Keys.ToArray();
            }
        }
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