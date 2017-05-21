using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class NewXamagramItemViewModel : ViewModelBase
    {
        private string _imageUrl;
        private string _name;
        private string _description;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand SaveCommand => new Command(SaveAsync);

        public ICommand CancelCommand => new Command(Cancel);

        public ICommand CameraCommand => new Command(CameraAsync);

        private async void SaveAsync()
        {
            var newXamagramItem = new XamagramItem
            {
                Name = Name,
                Image = ImageUrl,
                Description = Description
            };

            await XamagramMobileService.Instance.AddOrUpdateXamagramItemAsync(newXamagramItem);

            NavigationService.Instance.NavigateBack();
        }

        private async void CameraAsync()
        {
            var result = await PhotoService.Instance.TakePhotoAsync();
            
            if (result != null)
            {
                var imageUrl = await BlobService.Instance.UploadPhotoAsync(result);
                ImageUrl = imageUrl;
            }
        }

        private void Cancel()
        {
            NavigationService.Instance.NavigateBack();
        }
    }
}
