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
	[Register ("UploadTableViewCell")]
	partial class UploadTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnUpload { get; set; }

		[Outlet]
		UIKit.UILabel labelPercentage { get; set; }

		[Outlet]
		UIKit.UILabel labelUploadName { get; set; }

		[Outlet]
		UIKit.UIProgressView progressUpload { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnUpload != null) {
				btnUpload.Dispose ();
				btnUpload = null;
			}

			if (labelUploadName != null) {
				labelUploadName.Dispose ();
				labelUploadName = null;
			}

			if (progressUpload != null) {
				progressUpload.Dispose ();
				progressUpload = null;
			}

			if (labelPercentage != null) {
				labelPercentage.Dispose ();
				labelPercentage = null;
			}
		}
	}
}
