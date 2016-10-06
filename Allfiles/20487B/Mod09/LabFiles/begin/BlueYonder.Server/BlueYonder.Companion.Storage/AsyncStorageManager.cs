using BlueYonder.Companion.Storage.TableEntities;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlueYonder.Companion.Storage
{
    public class AsyncStorageManager
    {
        private CloudStorageAccount _account;
        private static readonly string MetadataTable = "FilesMetadata";

        public Uri BlobAddress
        {
            get { return _account.BlobEndpoint; }
        }

        public AsyncStorageManager()
        {
        }

        public async Task<Uri> UploadStreamAsync(string containerName, string fileName, Stream data, bool isPublic)
        {
            CloudBlockBlob blob = GetBlob(containerName, fileName, isPublic);

            // Note: The Task.Factory.FromAsync allows us to create a Task that represent the async operation
            // that is implemented using the Asynchronous Programming Model. this will allow any code calling 
            // the UploadPublicStreamAsync to use the await keyword and a more readable asynchronous code
            await Task.Factory.FromAsync(blob.BeginUploadFromStream,
                                         blob.EndUploadFromStream,
                                         data,
                                         null,
                                         TaskCreationOptions.None);
            return blob.Uri;
        }

        public async Task SaveMetadataAsync(FileEntity fileData)
        {
            TableServiceContext tableContext = GetTableContext();

            // TODO: Lab 9, Exercise 2: Task 1.3: use a TableServiceContext to add the object
            
            await Task.Factory.FromAsync<DataServiceResponse>(tableContext.BeginSaveChanges,
                                                              tableContext.EndSaveChanges,
                                                              null,
                                                              TaskCreationOptions.None);
        }

        private TableServiceContext GetTableContext()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileEntity> GetLocationMetadata(string locationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileEntity> GetFilesMetadata(IEnumerable<string> rowKeys)
        {
            
            TableServiceContext tableContext = GetTableContext();
            foreach (var rowKey in rowKeys)
            {
                var fileEntity = (from file in tableContext.CreateQuery<FileEntity>(MetadataTable)                                  
                                   //where file.RowKey == rowKey
                                  select file).Single();

                yield return fileEntity;
            }

        }      

        public string CreateSharedAccessSignature(string containerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Uri> GetFileUris(string containerName)
        {
            var container = GetContainer(containerName);
            return container.ListBlobs().Select(item => item.Uri);
        }

        private CloudBlobContainer GetContainer(string containerName)
        {
            throw new NotImplementedException();
        }

        private CloudBlockBlob GetBlob(string containerName, string fileName, bool isPublic)
        {
            throw new NotImplementedException();
        }
    }
}