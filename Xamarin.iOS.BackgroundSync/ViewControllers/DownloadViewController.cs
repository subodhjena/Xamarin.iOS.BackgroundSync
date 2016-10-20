using System;

using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class DownloadViewController : UIViewController
    {
        public DownloadViewController(IntPtr handle) : base("DownloadViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
           
        }
    }
}

