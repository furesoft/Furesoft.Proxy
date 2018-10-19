using System;

namespace Furesoft.Proxy.Core
{
    public class SearchPopupItem : BaseViewModel
    {
        public string Title { get; set; }

        private PopupItemType popupItemType;
        public PopupItemType PopupType
        {
            get { return popupItemType; }
            set { popupItemType = value; OnPropertyChanged(); }
        }


        public event EventHandler ItemClicked = delegate { };
    }
    public enum PopupItemType
    {
        Page,
        Setting,
        Action
    }
}