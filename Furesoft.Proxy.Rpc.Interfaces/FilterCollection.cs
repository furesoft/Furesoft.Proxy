using Furesoft.Proxy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Furesoft.Proxy.Rpc.Interfaces
{
    
    public class FilterCollection : ObservableCollection<Filter>
    {
        private ListBox filterLb;

        public FilterCollection()
        {
            
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Application.Current?.Dispatcher.Invoke(() =>
                   filterLb?.Items.Add((Filter)e.NewItems[0]));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Application.Current?.Dispatcher.Invoke(() =>
                    filterLb?.Items.Remove((Filter)e.OldItems[0])
                    );
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Application.Current?.Dispatcher.Invoke(() =>
                    filterLb?.Items.Clear());
                    break;
                default:
                    break;
            }
        }

        public void Replace(Filter filter)
        {
            for (int i = 0; i < Count - 1; i++)
            {
                if(this[i].Id == filter.Id)
                {
                    this[i] = filter;
                    filterLb.Items[i] = filter;
                    break;
                }
            }
            
            OnCollectionChanged(null);
        }

        public void AddRange(IEnumerable<Filter> enumerable)
        {
            foreach (var item in enumerable)
            {
                Add(item);
            }
        }

        public FilterCollection(IEnumerable<Filter> enumerable)
        {
            AddRange(enumerable);
        }

        public FilterCollection(ListBox filterLb)
        {
            this.filterLb = filterLb;
        }
    }
}