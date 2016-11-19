using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public class SyncManagerDelegate : NSUrlSessionDownloadDelegate
    {
        private WeakReference<SyncManager> weakSyncManager;

        public SyncManagerDelegate(SyncManager syncManager)
        {
            this.SyncManager = syncManager;
        }

        public SyncManagerDelegate()
        {
        }

        public SyncManager SyncManager
        {
            get
            {
                SyncManager syncManager = null;
                this.weakSyncManager.TryGetTarget(out syncManager);
                return syncManager;
            }
            set
            {
                this.weakSyncManager = new WeakReference<SyncManager>(value);
            }
        }

        public override void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
        {
            Console.WriteLine(string.Format("DidCompleteWithError TaskId: {0} {1}", task.TaskIdentifier, error == null ? string.Empty : " Error: " + error.Description));
            var appDel = UIApplication.SharedApplication.Delegate as AppDelegate;

            if (error == null)
            {
                appDel.SyncCompleted(task);
            }
            else
            {
                var taskId = Convert.ToInt32(task.TaskIdentifier);
                this.SyncManager.UpdateSyncStatus(taskId, SyncStatus.Failed);
              
                InvokeOnMainThread(delegate
                {
                    appDel.NotificationManager.ShowUploadNotification(false, "Failed For Task ID "+ taskId + " Reason :" + error.Description);
                });
            }
        }

        public override void DidBecomeInvalid(NSUrlSession session, NSError error)
        {
            Console.WriteLine("DidBecomeInvalid" + (error == null ? "undefined" : error.Description));
        }

        public override void DidFinishEventsForBackgroundSession(NSUrlSession session)
        {
            Console.WriteLine("DidFinishEventsForBackgroundSession");
        }

        public override void DidSendBodyData(NSUrlSession session, NSUrlSessionTask task, long bytesSent, long totalBytesSent, long totalBytesExpectedToSend)
        {
            var syncPercentage = ((double)totalBytesSent / totalBytesExpectedToSend) * 100.0;
            var taskId = Convert.ToInt32(task.TaskIdentifier);
            this.SyncManager.UpdateSyncProgress(taskId, syncPercentage);
        }

        // This is for Download

        public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
        {
            Console.WriteLine("Downloading Completed For {0}", downloadTask.TaskIdentifier);
        }

        public override void DidResume(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long resumeFileOffset, long expectedTotalBytes)
        {
            Console.WriteLine("Downloading Resumed For {0}", downloadTask.TaskIdentifier);
        }

        public override void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
        {
            var syncPercentage = ((double)totalBytesWritten / totalBytesExpectedToWrite) * 100.0;
            var taskId = Convert.ToInt32(downloadTask.TaskIdentifier);
            this.SyncManager.UpdateSyncProgress(taskId, syncPercentage);
        }
    }
}

