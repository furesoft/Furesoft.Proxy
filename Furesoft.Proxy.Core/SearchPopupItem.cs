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

        private bool _isFav;
        public bool IsFavourite
        {
            get { return _isFav; }
            set {
                _isFav = value;
                if(value)
                {
                    CommandUsageProvider.Instance.AddFavorite(Title);
                }
                else
                {
                    CommandUsageProvider.Instance.RemoveFavorite(Title);
                }
                OnPropertyChanged(); }
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