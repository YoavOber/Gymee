
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Services
{
    class GoogleDriveUploader
    {
        string[] Scopes = { DriveService.Scope.DriveFile };
        static void Upload_ProgressChanged(Google.Apis.Upload.IUploadProgress progress) =>
            Console.WriteLine(progress.Status + " " + progress.BytesSent);

        static void Upload_ResponseReceived(Google.Apis.Drive.v3.Data.File file) =>
            Console.WriteLine(file.Name + " was uploaded successfully");

        GoogleCredential creds;
        string folderId;
        DriveService drive;
        string credsPath = ConfigurationService.GetConfiguration().DriveCreds;
        void Initialize(string credsPath)
        {
            this.creds = GoogleCredential.FromStream(new FileStream(credsPath, FileMode.Open, FileAccess.Read)).CreateScoped(Scopes);
            this.folderId = null;

            // Create Drive API service.
            this.drive = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = creds
            });
        }
        public GoogleDriveUploader()
        {
            this.Initialize(credsPath);
        }
        public GoogleDriveUploader(string folderId)
        {
            this.Initialize(credsPath);
            this.folderId = folderId;
        }

        //for .mp4: video/mp4
        //for .ndp: text/basic
        public async Task Upload(string filePath, string uploadName, string contentType)
        {
            using (var uploadThis = System.IO.File.OpenRead(filePath))
            {
                var driveFile = new Google.Apis.Drive.v3.Data.File
                {
                    Name = uploadName,
                    Parents = this.folderId == null ? null : new List<string>() { folderId }
                };

                var uploadRequest = drive.Files.Create(driveFile, uploadThis, contentType);
                uploadRequest.ProgressChanged += Upload_ProgressChanged;
                uploadRequest.ResponseReceived += Upload_ResponseReceived;

                await uploadRequest.UploadAsync();
            }
        }


        public void MoveFolder(string folderId) {
            this.folderId = folderId;
        }

        public async Task<string> CreateFolder(string folderName) {
            var folderRequest = this.drive.Files.Create(new Google.Apis.Drive.v3.Data.File {
                Name = folderName,
                Parents = this.folderId == null ? null : new List<string>() { folderId },
                MimeType = "application/vnd.google-apps.folder"
            });

            var file = await folderRequest.ExecuteAsync();
            return file.Id;
        }

        public async Task<IList<Google.Apis.Drive.v3.Data.File>> GetFiles() {
            var files = await drive.Files.List().ExecuteAsync();
            return files.Files;
        }
    }
}
