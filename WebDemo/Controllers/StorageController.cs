using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity;
using WebDemo.Services;

using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.AspNetCore.Http;
using WebDemo.Common;
using Microsoft.Extensions.Configuration;

namespace WebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public StorageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<FileResponse>> upload()
        {
            var file = Request.Form.Files[0];

            String storageConnection = _configuration.GetValue<string>("BlobStorageConnectionString");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);

            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("eaar-storage");//container

            var fileGuid = Guid.NewGuid().ToString();

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            string fileName = fileGuid + Path.GetExtension(file.FileName);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = file.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return new FileResponse(fileName);
        }


        [HttpPost("download")]
        public async Task<IActionResult> download(FileRequest request)
        {
            String storageConnection = _configuration.GetValue<string>("BlobStorageConnectionString");

            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference("eaar-storage");
                blockBlob = cloudBlobContainer.GetBlockBlobReference(request.file);

                MemoryStream memStream = new MemoryStream();

                await blockBlob.DownloadToStreamAsync(memStream);
            }

            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);              
        }

        [HttpGet("all")]
        public async Task<List<BlobResponse>> ShowAllBlobs()
        {
            String storageConnection = _configuration.GetValue<string>("BlobStorageConnectionString");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("eaar-storage");
            CloudBlobDirectory dirb = container.GetDirectoryReference("eaar-storage");


            BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(string.Empty,
                true, BlobListingDetails.Metadata, 100, null, null, null);
            List<BlobResponse> fileList = new List<BlobResponse>();

            foreach (var blobItem in resultSegment.Results)
            {
                var blob = (CloudBlob)blobItem;
                fileList.Add(new BlobResponse()
                {
                    fileName = blob.Name,
                    fileSize = Math.Round((blob.Properties.Length / 1024f) / 1024f, 2).ToString(),
                    modifiedOn = DateTime.Parse(blob.Properties.LastModified.ToString()).ToLocalTime().ToString()
                });
            }

            return fileList;
        }
    }
}
