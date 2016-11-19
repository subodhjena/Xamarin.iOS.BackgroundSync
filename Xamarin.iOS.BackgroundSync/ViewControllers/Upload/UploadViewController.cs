using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;
using Realms;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class UploadViewController : UIViewController
    {
        private const int REFRESH_INTERVAL = 1;
        private NSTimer timer = null;
        private List<SyncModel> uploads;

        public UploadViewController(IntPtr handle) : base(handle)
        {
        }

        public List<SyncModel> SortedUploads
        {
            get
            {
                RefreshDataSource();
                return SortUploads();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tableView.Source = new UploadTableViewSource(this);
            this.tableView.EstimatedRowHeight = 60f;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // Create Timer
            if (timer == null)
            {
                timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(REFRESH_INTERVAL), delegate
                {
                    this.tableView.ReloadData();
                });
            }
           
            RefreshDataSource();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // Invalidate the Timer
            if (timer != null)
            {
                timer.Invalidate();
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void RefreshDataSource()
        {
            var realm = Realm.GetInstance();
            uploads = realm.All<SyncModel>().ToList();
        }

        private List<SyncModel> SortUploads()
        {
            var uploadsToSort = uploads;

            var sortedUploads = uploadsToSort.OrderBy(upload => upload.Status == (int)SyncStatus.Completed)
                                             .ThenBy(upload => upload.Status == (int)SyncStatus.Started)
                                             .ThenBy(upload => upload.Status == (int)SyncStatus.Failed).ToList();
            return sortedUploads;
        }

        partial void AddUpload(Foundation.NSObject sender)
        {
            var fileToUpload = Path.Combine(NSBundle.MainBundle.BundlePath, "SampleUpload.pdf");
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.SyncManager.StartUpload(fileToUpload);
        }
    }
}


