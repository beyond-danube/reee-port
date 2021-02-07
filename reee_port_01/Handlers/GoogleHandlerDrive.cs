using System.Collections.Generic;
using System.IO;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using File = Google.Apis.Drive.v3.Data.File;

namespace reeeport
{
    public class GoogleHandlerDrive : GoogleHandler
    {
        private DriveService service;

        public GoogleHandlerDrive()
        {
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public void UploadFile(string filePath, string folderId)
        {

            var file = new File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string>() { folderId }
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                request = service.Files.Create(file, stream, "");
                request.UploadAsync().Wait();
            }
        }

        public string CreateFolder(string folderName, string parentFolderId)
        {
            var folder = new File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>() { parentFolderId }
            };

            FilesResource.CreateRequest request = service.Files.Create(folder);

            var newFolder = request.Execute();

            return newFolder.Id;
        }
    }
}
