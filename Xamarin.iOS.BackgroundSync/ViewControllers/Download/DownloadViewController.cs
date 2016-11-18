using System;

using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class DownloadViewController : UIViewController
    {
        public DownloadViewController(IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // TableView
            this.tableView.Source = new DownloadTableViewSource();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    
        // Add a new Download
        partial void AddDownload(Foundation.NSObject sender)
        {

        }
    }
}

