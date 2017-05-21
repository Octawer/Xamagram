using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamagram.Services
{
    public class PhotoService
    {
        #region Singleton

        private static PhotoService _instance;

        public static PhotoService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhotoService();
                }

                return _instance;
            }
        }

        #endregion

        public async Task<MediaFile> PickPhotoAsync()
        {
            try
            {
                MediaFile image = null;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return image;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file != null && file.Path != null)
                {
                    image = file;
                }

                return image;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }

        public async Task<MediaFile> TakePhotoAsync()
        {
            try
            {
                MediaFile image = null;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return image;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                if (file != null)
                {
                    image = file;
                }

                return image;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }

    }
}
