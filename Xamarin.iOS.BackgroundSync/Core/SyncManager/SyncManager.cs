using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

using Foundation;

using Realms;
using System.Text;

namespace Xamarin.iOS.BackgroundSync
{
    public class SyncManager
    {
        private const string UploadAddress = "https://backsync.herokuapp.com/upload";
        private const string BackgroundSessionId = "SyncBackgroundSessionId";
        private const string FileExtension = ".txt";
        private const string BodyFileExtension = ".tmp";
        private NSUrlSession syncSession = null;

        public SyncManager()
        {
        }

        public NSUrlSession InitSyncSession()
        {
            ////See URL below for configuration options
            ////https://developer.apple.com/library/ios/documentation/Foundation/Reference/NSURLSessionConfiguration_class/index.html

            using (var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(BackgroundSessionId))
            {
                config.HttpMaximumConnectionsPerHost = 4; //iOS Default is 4
                config.TimeoutIntervalForRequest = 600.0; //30min allowance; iOS default is 60 seconds.
                config.TimeoutIntervalForResource = 120.0; //2min; iOS Default is 7 days
               
                return NSUrlSession.FromConfiguration(config, new SyncManagerDelegate(this), new NSOperationQueue());
            }
        }

        public void StartUpload(string filePath)
        {
            // Create a GUID
            var id = Guid.NewGuid().ToString();

            // New file path
            var newFileName = id + FileExtension;
            var newPath = Path.Combine(Utilities.BackgroundSyncFilePath(), newFileName);

            try
            {
                // Copy File
                File.Copy(filePath, newPath);

                // Create a SyncModel object so that we can track our Upload
                var realm = Realm.GetInstance();
                realm.Write(() =>
                {
                    var sync = realm.CreateObject<SyncModel>();
                    sync.Id = id;
                    sync.TaskIdentifier = 0;
                    sync.FilePath = newPath;
                    sync.SyncProgress = 0;
                    sync.Status = (int)SyncStatus.Stopped;
                    sync.SyncType = (int)SyncType.Upload;
                });

                // Start the Upload
                Upload(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Cannot Copy File & Start Download: {0}", ex.StackTrace);
            }
        }

        public void UpdateSyncProgress(int taskIdentifier, double syncProgress)
        {
            Console.WriteLine("Progress {0} for TaskID {1}", syncProgress, taskIdentifier);

            // Get the Upload
            var realm = Realm.GetInstance();
            var upload = realm.All<SyncModel>().FirstOrDefault(sync => sync.TaskIdentifier == taskIdentifier);

            realm.Write(() =>
            {
                upload.SyncProgress = syncProgress;
            });
        }

        public void UpdateSyncStatus(int taskIdentifier, SyncStatus syncStatus)
        {
            // Get the Upload
            var realm = Realm.GetInstance();
            var upload = realm.All<SyncModel>().FirstOrDefault(sync => sync.TaskIdentifier == taskIdentifier);

            realm.Write(() =>
            {
                if (syncStatus == SyncStatus.Stopped)
                {
                    upload.Status = (int)SyncStatus.Stopped;
                }
                else if (syncStatus == SyncStatus.Started)
                {
                    upload.Status = (int)SyncStatus.Started;
                }
                else if (syncStatus == SyncStatus.Completed)
                {
                    upload.TaskIdentifier = 0;
                    upload.Status = (int)SyncStatus.Completed;
                }
                else if (syncStatus == SyncStatus.Failed)
                {
                    upload.TaskIdentifier = null;
                    upload.Status = (int)SyncStatus.Failed;
                }
            });
        }

        private void Upload(string uploadId)
        {
            try
            {
                // Get the Upload
                var realm = Realm.GetInstance();
                var upload = realm.All<SyncModel>().FirstOrDefault(sync => sync.Id == uploadId);
                    
                if (this.syncSession == null)
                {
                    this.syncSession = this.InitSyncSession();
                }

                if (File.Exists(upload.FilePath))
                {
                    var boundary = "FileBoundary";
                    var bodyPath = Path.Combine(Utilities.BackgroundSyncFilePath(), upload.Id + BodyFileExtension);

                    // Create Upload Request
                    NSUrl uploadHandleUrl = NSUrl.FromString(UploadAddress);
                    NSMutableUrlRequest request = new NSMutableUrlRequest(uploadHandleUrl);
                    request.HttpMethod = "POST";
                    request["Content-Type"] = "multipart/form-data; boundary=" + boundary;
                    request["FileName"] = Path.GetFileName(upload.FilePath);

                    // Construct the Request body
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(string.Empty);
                    sb.AppendFormat("--{0}\r\n", boundary);
                    sb.AppendFormat("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\n", Path.GetFileName(upload.FilePath));
                    sb.Append("Content-Type: application/octet-stream\r\n\r\n");

                    // Delete any previous body data file
                    if (File.Exists(bodyPath))
                    {
                        File.Delete(bodyPath);
                    }

                    // Write file to BodyPart
                    var fileBytes = File.ReadAllBytes(upload.FilePath);
                    using (var writeStream = new FileStream(bodyPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        writeStream.Write(Encoding.Default.GetBytes(sb.ToString()), 0, sb.Length);
                        writeStream.Write(fileBytes, 0, fileBytes.Length);

                        sb.Clear();
                        sb.AppendFormat("\r\n--{0}--\r\n", boundary);
                        writeStream.Write(Encoding.Default.GetBytes(sb.ToString()), 0, sb.Length);
                    }

                    sb = null;
                    fileBytes = null;

                    // Creating upload task
                    var uploadTask = this.syncSession.CreateUploadTask(request, NSUrl.FromFilename(bodyPath));

                    // Save the Identifier to the Existing Upload
                    var taskId = Convert.ToInt32(uploadTask.TaskIdentifier);

                    realm.Write(() =>
                    {
                        upload.TaskIdentifier = taskId;
                    });

                    // Start task
                    uploadTask.Resume();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload Failed With Ex: {0}", ex.StackTrace);
            }
        }

        private async Task<NSUrlSessionTask> GetPendingUploadTask(nuint taskIdentifier)
        {
            NSUrlSessionTask uploadTask = null;

            if (this.syncSession != null)
            {
                var tasks = await this.syncSession.GetTasks2Async();

                var taskList = tasks.UploadTasks;
                if (taskList.Count() > 0)
                {
                    foreach (NSUrlSessionTask task in taskList)
                    {
                        if (task.TaskIdentifier == taskIdentifier)
                        {
                            uploadTask = task;
                        }
                    }
                }
            }

            return uploadTask;
        }
    }
}

