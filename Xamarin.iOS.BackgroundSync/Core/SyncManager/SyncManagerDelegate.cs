using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public class SyncManagerDelegate : NSUrlSessionTaskDelegate
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
            Console.WriteLine(string.Format("DidCompleteWithError TaskId: {0}{1}", task.TaskIdentifier, error == null ? string.Empty : " Error: " + error.Description));

            if (error == null)
            {
                var appDel = UIApplication.SharedApplication.Delegate as AppDelegate;
                appDel.SyncCompleted(task);
            }
            else
            {
                var taskId = Convert.ToInt32(task.TaskIdentifier);
                // this.SyncManager.UpdateUploadStatus(taskId, AuditUploadStatus.Failed);
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
            var uploadPercentage = ((double)totalBytesSent / totalBytesExpectedToSend) * 100.0;
            var taskId = Convert.ToInt32(task.TaskIdentifier);

            // this.SyncManager.UpdateUploadPercentage(taskId, uploadPercentage);
        }
    }
}

