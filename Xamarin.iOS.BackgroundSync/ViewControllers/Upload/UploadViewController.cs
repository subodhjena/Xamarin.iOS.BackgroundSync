using System;
using System.IO;

using UIKit;
using Foundation;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class UploadViewController : UIViewController
    {
        public UploadViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tableView.Source = new UploadTableViewSource();
            this.tableView.EstimatedRowHeight = 60f;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        // Add a new Upload
        partial void AddUpload(Foundation.NSObject sender)
        {
            var fileToUpload = Path.Combine(NSBundle.MainBundle.BundlePath, "SampleUpload.pdf");
            var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
            appDelegate.SyncManager.StartUpload(fileToUpload);
        }
    }
}


