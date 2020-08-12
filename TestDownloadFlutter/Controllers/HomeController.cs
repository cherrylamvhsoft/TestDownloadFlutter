using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TestDownloadFlutter.Models;

namespace TestDownloadFlutter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DownloadAPK()//void DownloadAPK()//IActionResult DownloadAPK()
        {
            CloudBlockBlob blockBlob;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = "DefaultEndpointsProtocol=https;AccountName=fluttertest;AccountKey=dJEFw6h2sepEblA6/nWRMvm5fGacRQg8Q7wdOSHCzzxwjsvGPNCYvRk1Fob3pblCf8DNXl/k7X/BnFXGF4vF3w==;EndpointSuffix=core.windows.net";
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontainer");
                blockBlob = cloudBlobContainer.GetBlockBlobReference("app-release.apk");
                blockBlob.DownloadToStreamAsync(memoryStream);
            }

            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
        }

        public IActionResult DownloadIPA()
        {
            CloudBlockBlob blockBlob;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = "DefaultEndpointsProtocol=https;AccountName=fluttertest;AccountKey=dJEFw6h2sepEblA6/nWRMvm5fGacRQg8Q7wdOSHCzzxwjsvGPNCYvRk1Fob3pblCf8DNXl/k7X/BnFXGF4vF3w==;EndpointSuffix=core.windows.net";
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontainer");
                blockBlob = cloudBlobContainer.GetBlockBlobReference("Runner.ipa");
                blockBlob.DownloadToStreamAsync(memoryStream);
            }

            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
        }

    }
}
