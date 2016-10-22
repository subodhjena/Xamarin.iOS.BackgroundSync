// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Xamarin.iOS.BackgroundSync
{
	[Register ("DownloadTableViewCell")]
	partial class DownloadTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnDownload { get; set; }

		[Outlet]
		UIKit.UILabel labelFileName { get; set; }

		[Outlet]
		UIKit.UIProgressView progressDownlaod { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelFileName != null) {
				labelFileName.Dispose ();
				labelFileName = null;
			}

			if (progressDownlaod != null) {
				progressDownlaod.Dispose ();
				progressDownlaod = null;
			}

			if (btnDownload != null) {
				btnDownload.Dispose ();
				btnDownload = null;
			}
		}
	}
}
