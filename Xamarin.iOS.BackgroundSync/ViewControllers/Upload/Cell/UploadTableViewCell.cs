using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class UploadTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("UploadTableViewCell");
        public static readonly UINib Nib;

        public UILabel Name
        {
            get { return labelUploadName; }
            set { labelUploadName = value; }
        }

        public UILabel Percentage
        {
            get { return labelPercentage; }
            set { labelPercentage = value; }
        }

        public UIProgressView Progress
        {
            get { return progressUpload; }
            set { progressUpload = value; }
        }

        public UIButton Download
        {
            get { return btnUpload; }
            set { btnUpload = value; }
        }

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
