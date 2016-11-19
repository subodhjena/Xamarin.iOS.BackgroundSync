using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;
using Realms;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class DownloadViewController : UIViewController
    {
        private const string DownloadAddress = "https://backsync.herokuapp.com/download";
        private const int REFRESH_INTERVAL = 1;
        private NSTimer timer = null;
        private List<SyncModel> downloads;

        public DownloadViewController(IntPtr handle) : base (handle)
        {
            // Create Timer
            if (timer == null)
            {
                timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(REFRESH_INTERVAL), delegate
                {
                    this.tableView.ReloadData();
                });
            }
        }

        public List<SyncModel> SortedDownloads
        {
            get
            {
                RefreshDataSource();
                return SortDownloads();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // TableView
            this.tableView.Source = new DownloadTableViewSource(this);
            this.tableView.EstimatedRowHeight = 60f;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void RefreshDataSource()
        {
            var realm = Realm.GetInstance();
            downloads = realm.All<SyncModel>().Where(upload => upload.SyncType == (int)SyncType.Download).ToList();
        }

        private List<SyncModel> SortDownloads()
        {
            var uploadsToSort = downloads;

            var sortedUploads = uploadsToSort.OrderBy(upload => upload.Status == (int)SyncStatus.Completed)
                                             .ThenBy(upload => upload.Status == (int)SyncStatus.Started)
                                             .ThenBy(upload => upload.Status == (int)SyncStatus.Failed).ToList();
            return sortedUploads;
        }
    
        // Add a new Download
        partial void AddDownload(Foundation.NSObject sender)
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.SyncManager.StartDownload(DownloadAddress);
        }
    }
}

