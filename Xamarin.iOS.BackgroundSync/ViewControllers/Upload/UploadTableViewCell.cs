using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class UploadTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("UploadTableViewCell");
        public static readonly UINib Nib;

        static UploadTableViewCell()
        {
            Nib = UINib.FromName("UploadTableViewCell", NSBundle.MainBundle);
        }

        protected UploadTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public static UploadTableViewCell Create()
        {
            return (UploadTableViewCell)Nib.Instantiate(null, null)[0];
        }
    }
}
