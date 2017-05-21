using Microsoft.WindowsAzure.Storage;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamagram.Services
{
    public class BlobService
    {
        #region Singleton

        private static readonly Lazy<BlobService> lazy = new Lazy<BlobService>(() => new BlobService());

        public static BlobService Instance { get { return lazy.Value; } }

        private BlobService()
        {
        }

        #endregion

        public async Task<string> UploadPhotoAsync(MediaFile photo)
        {
            // Conectar con la cuenta Azure Storage.
            // NOTA: Se deben utilizar tokens SAS en lugar de Shared Keys en aplicaciones en producción.
            var storageAccount = CloudStorageAccount.Parse(GlobalSettings.BlobSharedKey);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Crear el contenedor blob si no existe.
            var container = blobClient.GetContainerReference(GlobalSettings.BlobContainerName);
            await container.CreateIfNotExistsAsync();

            // Subimos el blob a Azure Storage.
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.Properties.ContentType = "image/png";
            await blob.UploadFromStreamAsync(photo.GetStream());

            return blob.Uri.ToString();
        }
    }
}
