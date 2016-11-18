using System;

using UIKit;

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
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        // Add a new Upload
        partial void AddUpload(Foundation.NSObject sender)
        {

        }
    }
}


