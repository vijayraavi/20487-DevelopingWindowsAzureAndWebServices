using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueYonder.Companion.Client.DataModel;
using BlueYonder.Companion.Shared;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace BlueYonder.Companion.Client.Helpers
{
    public class TransferManager
    {
        private readonly DataManager _data = new DataManager();

        public async Task UploadAsync(StorageFile file, int travelerId, Progress<UploadOperation> progressCallback, int reservationId, bool isPrivate)
        {
            var address = isPrivate ? Addresses.UploadPrivateFileUri : Addresses.UploadPublicFileUri;
            Uri destination = new Uri(string.Format(address, reservationId));

            BackgroundUploader uploader = new BackgroundUploader();
            uploader.SetRequestHeader("Filename", file.Name);
            UploadOperation upload = uploader.CreateUpload(destination, file);

            UploadOperation result = await upload.StartAsync().AsTask();

            string fileUri = result.GetResponseInformation().Headers["Location"];
            await _data.CreateAzureStorageFileMetadata(fileUri, file.Name, reservationId, isPrivate, UserAuth.Instance.Traveler.TravelerId);
        }

        public async Task DownloadAsync(Uri uri, StorageFile destination)
        {
            var downloader = new BackgroundDownloader();
            var download = downloader.CreateDownload(uri, destination);

            await download.StartAsync();
        }

        //Module 10 - Background Tasks
        //The student will be able to create and consume background tasks.
        public async Task ResumeDownloads()
        {
            IReadOnlyList<DownloadOperation> downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();
            if (downloads.Count > 0)
            {
                foreach (DownloadOperation download in downloads)
                {
                    if (download.Progress.Status != BackgroundTransferStatus.Running)
                    {
                        download.Resume();
                    }
                }
            }
        }
    }
}