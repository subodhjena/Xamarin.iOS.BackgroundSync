using System;

using Foundation;
using UIKit;

namespace Xamarin.iOS.BackgroundSync
{
    public partial class DownloadTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DownloadTableViewCell");
        public static readonly UINib Nib;

        public UILabel Name
        {
            get { return labelFileName; }
            set { labelFileName = value; }
        }

        public UILabel Percentage
        {
            get { return labelPercentage;}
            set { labelPercentage = value;}
        }

        public UIProgressView Progress
        {
            get { return progressDownlaod; }
            set { progressDownlaod = value; }
        }

        public UIButton Download
        {
            get { return btnDownload; }
            set { btnDownload = value; }
        }

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
