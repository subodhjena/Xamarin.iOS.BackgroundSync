using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class DownloadTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DownloadTableViewCell");
        public static readonly UINib Nib;

        static DownloadTableViewCell()
        {
            Nib = UINib.FromName("DownloadTableViewCell", NSBundle.MainBundle);
        }

        protected DownloadTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public static DownloadTableViewCell Create()
        {
            return (DownloadTableViewCell)Nib.Instantiate(null, null)[0];
        }
    }
}
