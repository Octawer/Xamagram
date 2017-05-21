using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class XamagramItemsViewModel : ViewModelBase
    {
        private ObservableCollection<XamagramItem> _items;
        private XamagramItem _selectedItem;

        public ObservableCollection<XamagramItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public XamagramItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                // Creando un servicio de navegación
                NavigationService.Instance.NavigateTo<XamagramItemDetailViewModel>(_selectedItem);
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var result = await XamagramMobileService.Instance.ReadXamagramItemsAsync();

            if(result != null)
            {
                Items = new ObservableCollection<XamagramItem>(result);
            }
        }

        public ICommand NewCommand => new Command(New);

        private void New()
        {
            NavigationService.Instance.NavigateTo<NewXamagramItemViewModel>();
        }
    }
}