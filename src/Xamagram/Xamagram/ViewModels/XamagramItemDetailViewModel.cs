using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class XamagramItemDetailViewModel : ViewModelBase
    {
        private XamagramItem _item;

        public XamagramItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if(navigationContext is XamagramItem)
            {
                Item = (XamagramItem)navigationContext;
            }
        }

        public ICommand DeleteCommand => new Command(DeleteAsync);

        private async void DeleteAsync()
        {
            if (Item.Id != null)
            {
                await XamagramMobileService.Instance.DeleteXamagramItemAsync(Item);

                NavigationService.Instance.NavigateBack();
            }
        }
    }
}