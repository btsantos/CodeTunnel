using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CodeTunnel.UI.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public FileResult Index(string blobName, string containerName = "root")
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blob = container.GetBlobReference(blobName);
            var binaryBlob = blob.DownloadByteArray();
            return File(binaryBlob, blob.Attributes.Properties.ContentType);
        }

        public string SetupStorage()
        {
            try
            {
                var status = new StringBuilder();

                var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                var blobClient = storageAccount.CreateCloudBlobClient();

                var container = blobClient.GetContainerReference("Misc");
                container.CreateIfNotExist();
                status.Append("Misc container created.<br /><br />");
                var rootFiles = Directory.GetFiles(Server.MapPath("~/content"), "*.*", SearchOption.TopDirectoryOnly);
                foreach (var file in rootFiles)
                {
                    var blob = container.GetBlobReference(file);
                    using (var fileStream = System.IO.File.OpenRead(Server.MapPath("~/content/" + file)))
                    {
                        blob.UploadFromStream(fileStream);
                    }
                }
                status.Append("Root files added to Misc container as blobs.<br />");

                Action<string, string> addFilesInDirectory = null;
                addFilesInDirectory = (directoryName, path) =>
                    {
                        container = blobClient.GetContainerReference(directoryName);
                        container.CreateIfNotExist();
                        status.Append(directoryName + " container created.<br /><br />");

                        var files = Directory.GetFiles(Server.MapPath(path), "*.*", SearchOption.TopDirectoryOnly);
                        foreach (var file in files)
                        {
                            var blob = container.GetBlobReference(file);
                            using (var fileStream = System.IO.File.OpenRead(path + file))
                            {
                                blob.UploadFromStream(fileStream);
                            }
                            status.Append(file + " added to " + directoryName + " container.<br />");
                        }


                        foreach (var directory in Directory.GetDirectories(Server.MapPath(path)))
                            addFilesInDirectory(directory, Server.MapPath(path + "/" + directory + "/"));
                    };

                foreach (var directory in Directory.GetDirectories(Server.MapPath("~/")))
                    addFilesInDirectory(directory, "~/" + directory + "/");

                status.Append("<br />Setup successful.");
                return status.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
